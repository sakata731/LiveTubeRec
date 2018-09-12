using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LiveTubeReport {
	public class ChannelManager {
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private YouTubeDataProvider _youTubeDataProvider;
		private Schedule _schedule;
		private DataTable _channelTable;
		private ProcessExe _process;

		public ChannelManager(DataTable dataTable, Schedule schedule,
			YouTubeDataProvider youTubeDataProvider, ProcessExe process) {
			_channelTable = dataTable;
			_schedule = schedule;
			_youTubeDataProvider = youTubeDataProvider;
			_process = process;
		}

		//メインロジック
		public void DoBaseLogic() {
			logger.Trace("");

			for (int i = 0; i < _channelTable.Rows.Count; i++) {
				//録画中ならリクエストしない
				if ((bool)_channelTable.Rows[i]["appStat"] == true) {
					logger.Info("チャンネル " + _channelTable.Rows[i]["channelName"].ToString() + " は外部アプリが実行中のため記録を終了します。");
					continue;
				}

				DateTime dt = DateTime.Now;

				int[] scheduleArray = _schedule.scheduleList[dt.Hour];

				if(0 == scheduleArray.Length || 0 > Array.IndexOf(scheduleArray, dt.Minute)) {//indexof 引数が要素の中で何番目にあるかを返す
					logger.Info("チャンネル " + _channelTable.Rows[i]["channelName"].ToString() + " はスケジュールされていないので記録を終了します。");
					continue;
				}

				//書き方あってる？
				//await Task.Run(() => {
				//	this.SetLiveStatus(_channelTable.Rows[i]);
				//});

				this.SetLiveStatus(_channelTable.Rows[i]);

				if ((bool)_channelTable.Rows[i]["liveStatus"] == true) {
					this.executeAplication(_channelTable.Rows[i]["channelID"].ToString(), _channelTable.Rows[i]["liveID"].ToString());
					_channelTable.Rows[i]["appStat"] = true;
				}
			}
		}

		public void ForceDoLogic() {
			logger.Trace("");
			
			for (int i = 0; i < _channelTable.Rows.Count; i++) {
				
				this.SetLiveStatus(_channelTable.Rows[i]);
			
				if ((bool)_channelTable.Rows[i]["liveStatus"] == true) {
					string channelName = _channelTable.Rows[i]["channelName"].ToString();

					DialogResult result = new DialogResult();
					if ((bool)_channelTable.Rows[i]["appStat"] == true) {
						 result = MessageBox.Show("チャンネル " + channelName + "\r\n" + "外部アプリが起動しています。\r\n" + "外部アプリを起動しますか？", "外部アプリを起動しますか？"
								, MessageBoxButtons.YesNoCancel
								, MessageBoxIcon.Question
								, MessageBoxDefaultButton.Button2);

						if(result != DialogResult.Yes) {
							continue;
						}
					}

					this.executeAplication(_channelTable.Rows[i]["channelID"].ToString(), _channelTable.Rows[i]["liveID"].ToString());
					_channelTable.Rows[i]["appStat"] = true;
				}
			}
		}

		public DataRow GetPreparedDataRow(string channelID, DataTable table) {
			logger.Trace("");

			Dictionary<string, object> dic;
			dic = this._youTubeDataProvider.RequestChannelData(channelID);

			DataRow row = table.NewRow();

			row["channelID"] = channelID;
			row["channelName"] = dic["channelName"];
			row["addDate"] = DateTime.Now;

			string url = dic["thumbnail"].ToString();
			string path = @".\data\image\" + channelID + ".png";

			row["thumbnailUrl"] = url;
			row["thumbnailPath"] = path;

			if (!System.IO.File.Exists(path)) {
				Bitmap bmp = new Bitmap(Utils.loadImageFromURL(url));
				bmp.Save(@".\data\image\" + channelID + ".png", System.Drawing.Imaging.ImageFormat.Png);
			}

			return row;
		}

		//datarowにチャンネル情報を設定し、ライブ情報から外部アプリを実行する
		public void SetLiveStatusAndExecApp(DataRow row) {
			this.SetLiveStatus(row);

			if ((bool)row["liveStatus"] == true) {
				this.executeAplication(row["channelID"].ToString(), row["liveID"].ToString());
				row["appStat"] = true;
			}
		}
		public void SetLiveStatus(DataRow row) {
			logger.Debug("チャンネル " + row["channelName"].ToString() + " の記録を開始します。");

			Dictionary<string, object> dic = _youTubeDataProvider.RequestLiveData(row["channelID"].ToString());

			DateTime dt = DateTime.Now;
			row["liveStatus"] = dic["liveStatus"];
			if ((bool)dic["liveStatus"]) {
				row["liveID"] = dic["liveID"];
				row["liveTitle"] = dic["liveTitle"];
				row["liveUrl"] = dic["liveUrl"];
				row["liveStartTime"] = dt;

				logger.Info("チャンネル " + row["channelName"].ToString() + " 配信中です。");
			}
			else {
				logger.Info("チャンネル " + row["channelName"].ToString() + " の配信はありません。");
			}

			row["liveLastRequestTime"] = dt;

			logger.Debug("チャンネル " + row["channelName"].ToString() + " の記録を終了します。");
		}

		private void executeAplication(string channelID, string liveUrl) {
			
			ProcessStartInfo processInfo = new ProcessStartInfo(@"youtube-dl.exe");
			processInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\youtube-dl";
			processInfo.Arguments = "\"" + liveUrl + "\"";

			_process.StartInfo = processInfo;

			_process.ChannelID = channelID;

			//イベントハンドラの追加
			_process.Exited += new EventHandler(p_Exited);

			//起動する
			_process.Start();
			logger.Info("youtube-dl.exe を起動します。");
		}

		//プロセスが終了したときに実行される
		private void p_Exited(object sender, EventArgs e) {
			logger.Trace("");

			string channelID = ((ProcessExe)sender).ChannelID;

			for (int i = 0; i < _channelTable.Rows.Count; i++) {
				if (channelID.Equals(_channelTable.Rows[i]["channelID"].ToString())) {
					_channelTable.Rows[i]["appStat"] = false;
					_channelTable.Rows[i]["liveEndTime"] = DateTime.Now;
					break;
				}
			}
		}
	}
}
