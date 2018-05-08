using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace LiveTubeRec
{
	public class ChannelManager
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private YouTubeDataProvider _youTubeDataProvider;
		private Schedule _schedule;
		private DataTable _channelTable;
		private Process _process;

		public ChannelManager(DataTable dataTable, Schedule schedule, 
			YouTubeDataProvider youTubeDataProvider, Process process)
		{
			_channelTable = dataTable;
			_schedule = schedule;
			_youTubeDataProvider = youTubeDataProvider;
			_process = process;
		}

		//メインロジック
		public void DoBaseLogic()
		{
			logger.Trace("");

			for (int i = 0; i < _channelTable.Rows.Count; i++)
			{
				logger.Trace("index : " + i);

				//録画中ならリクエストしない
				if ((bool)_channelTable.Rows[i]["appStat"] == true)
				{
					_channelTable.Rows[i]["liveStatus"] = false;
					continue;
				}
				
				this.SetLiveStatus(_channelTable.Rows[i], _schedule);

				if ((bool)_channelTable.Rows[i]["liveStatus"] == true)
				{
					this.executeAplication(_channelTable.Rows[i]["channelID"].ToString(),_channelTable.Rows[i]["liveID"].ToString());
					_channelTable.Rows[i]["appStat"] = true;
				}
			}
		}

		public DataRow GetPreparedDataRow(string channelID, DataTable table)
		{
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

			if (!System.IO.File.Exists(path))
			{
				Bitmap bmp = new Bitmap(Utils.loadImageFromURL(url));
				bmp.Save(@".\data\image\" + channelID + ".png", System.Drawing.Imaging.ImageFormat.Png);
			}

			return row;
		}

		//スケジューリングをもとにYouTubeDataProviderをリクエストしてデータrowにセットする
		public void SetLiveStatus(DataRow row, Schedule schedule)
		{
			logger.Trace("");

			if ((bool)row["appStat"] != true)
			{
				DateTime dt = DateTime.Now;

				int[] scheduleArray = _schedule.scheduleList[dt.Hour];
				if (0 != scheduleArray.Length
					&& (-1 == scheduleArray[0] || 0 <= Array.IndexOf(scheduleArray, dt.Minute))) //indexof 引数が要素の中で何番目にあるかを返す
				{
					this.SetLiveStatus(row);
				}
			}
		}

		//YouTubeDataProviderをリクエストしてデータrowにセットする
		public void SetLiveStatus(DataRow row)
		{
			logger.Trace("");

			Dictionary<string, object> dic = _youTubeDataProvider.RequestLiveData(row["channelID"].ToString());

			DateTime dt = DateTime.Now;
			row["liveStatus"] = dic["liveStatus"];
			if ((bool)dic["liveStatus"])
			{
				row["liveID"] = dic["liveID"];
				row["liveTitle"] = dic["liveTitle"];
				row["liveUrl"] = dic["liveUrl"];
				row["liveStartTime"] = dt;
			}

			row["liveLastRequestTime"] = dt;
		}

		private void executeAplication(string channelID, string liveUrl)
		{
			logger.Trace("start");

			//ProcessStartInfo processInfo = new ProcessStartInfo(@".\youtube-dl.exe");
			ProcessStartInfo processInfo = new ProcessStartInfo("notepad.exe");
			processInfo.Arguments = "";

			_process.StartInfo = processInfo;

			//イベントハンドラの追加
			_process.Exited += new EventHandler(p_Exited);

			logger.Trace("process start");

			//起動する
			_process.Start();
		}

		//プロセスが終了したときに実行される
		private void p_Exited(object sender, EventArgs e)
		{
			logger.Trace("start");

			for(int i = 0; i < _channelTable.Rows.Count; i++)
			{
				if(_channelTable.Rows[i]["live"])
			}
		}
	}
}
