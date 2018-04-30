using System;
using System.Windows.Forms;
using NLog;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Timers;
using System.Diagnostics;

/*
 * 更新日 2018/04/23
 *
 */

namespace LiveTubeRec
{
	public partial class Form1 : Form
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private static ChannelManager _ChannelManager;

		private static System.Timers.Timer _Timer;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//データセットの初期化
			liveTubeDataSet.Clear();
			DirectoryUtils.SafeCreateDirectory(@".\data");
			DirectoryUtils.SafeCreateDirectory(@".\data\image");

			if (System.IO.File.Exists(@".\data\data.xml"))
			{
				liveTubeDataSet.ReadXml(@".\data\data.xml");
				logger.Info("データの読み込みに成功しました。");
			}
			else
			{
				liveTubeDataSet.WriteXml(@".\data\data.xml");
			}

			for(int i = 0; i < channelTable.Rows.Count; i++)
			{
				channelTable.Rows[i]["liveStatus"] = false;
				channelTable.Rows[i]["appStat"] = false;
			}


			//データグリッドビューの初期処理
			this.replaceDataGridView();
			this.statusSetToDataGridView();

			//チャンネルマネージャの初期処理
			_ChannelManager = new ChannelManager(@".\config\config.ini");

			_Timer = new System.Timers.Timer();
			_Timer.Elapsed += new System.Timers.ElapsedEventHandler(doMonitaring);
			_Timer.Interval = 30000; //msec

			_Timer.Start();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			DirectoryUtils.SafeCreateDirectory(@".\data");
			liveTubeDataSet.WriteXml(@".\data\data.xml");
		}

		//<summary>
		// 追加ボタン
		//</summary>
		private void buttonInsert_Click(object sender, EventArgs e)
		{
			if ("".Equals(textBoxChannelID.Text)) return;

			string channelID = this.getChannelIDByUrl(textBoxChannelID.Text);
			if (!"".Equals(channelID) && !hasChannelID(channelID))
			{
				DataRow row = _ChannelManager.GetPreparedDataRow(channelID, channelTable);
				_ChannelManager.UpdateChanelData(row);

				channelTable.Rows.Add(row);
			}
			else
			{
				logger.Error("入力したURLが正しくないか、一覧に存在しているため追加できません。");
			}

			this.replaceDataGridView();
			this.doMonitaring();

			textBoxChannelID.Text = "";
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				for (int i = 0; i < channelTable.Rows.Count; i++)
				{
					var a = channelTable.Rows[i]["channelID"];
					var b = dataGridView.SelectedRows[0].Cells["dgvChannelID"].Value;
					if (channelTable.Rows[i]["channelID"].Equals(dataGridView.SelectedRows[0].Cells["dgvChannelID"].Value))
					{
						channelTable.Rows[i].Delete();
					}
				}
			}
		}

		private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dataGridView.SelectedRows.Count < 1)
			{
				toolStripMenuItemDelete.Enabled = false;
			}
			else
			{
				toolStripMenuItemDelete.Enabled = true;
			}
		}

		private void textBoxChannelID_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				buttonInsert_Click(sender, e);
			}
		}

		private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			//"Link"列ならば、ボタンがクリックされた
			if (dgv.Columns[e.ColumnIndex].Name == "dgvLiveURL")
			{
				//訪問済みにする
				DataGridViewLinkCell cell =
					(DataGridViewLinkCell)dgv[e.ColumnIndex, e.RowIndex];
				cell.LinkVisited = true;
				Process.Start(cell.Value.ToString());
			}
		}


		//リストにチャンネルIDが登録されているかをチェック
		// 登録済み:true 未登録:false
		private bool hasChannelID(string inputCannelID)
		{
			logger.Debug("start");

			bool rtn = false;
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				// 編集中の行どうしでないとき
				if (row.Cells["dgvChannelID"].Value != null && row.Cells["dgvChannelID"].Value.Equals(inputCannelID))
				{
					rtn = true;
				}
			}

			return rtn;
		}

		//URLからチェンネルIDを取得 取得できなければ空文字を返す
		private string getChannelIDByUrl(string inputURL)
		{
			logger.Debug("Start");

			string expression = "(?<type>channel)/(?<id>.*?)(&|$|/)";

			Regex reg = new Regex(expression);
			Match match = reg.Match(inputURL);
			bool rtn = match.Success;
			if (rtn == true)
			{
				return match.Groups["id"].Value;
			}
			else
			{
				return "";
			}
		}

		//datagridviewの列ごとの処理を記述
		private void replaceDataGridView()
		{
			logger.Trace("start");
			
			for(int i = 0; i < dataGridView.Rows.Count; i++)
			{
				//チャンネルIDからレコードを取得
				DataRow[] rows = liveTubeDataSet.Tables[0].Select("ChannelID = " + "'" + dataGridView.Rows[i].Cells["dgvChannelID"].Value.ToString() + "'");

				//チャンネルIDは一意
				if (rows.Length == 1)
				{
					//サムネイルをセット
					object o = rows[0]["thumbnailPath"];
					if (o != null && !"".Equals(o.ToString()))
					{
						dataGridView.Rows[i].Cells["dgvThumbnail"].Value = new Bitmap(o.ToString());
					}
				}
				else
				{
					logger.Error("チャンネルデータに誤りがあります。");
					break;
				}
			}
		}

		private void doMonitaring(object sender, ElapsedEventArgs e)
		{
			this.doMonitaring();
		}

		private void doMonitaring()
		{
			logger.Trace("start");
			_ChannelManager.UpdateChanelData(ref channelTable);

			logger.Debug("\r\nbefor");
			loggingLiveData();

			//他の方法が思いつかない
			for (int i = 0; i < channelTable.Rows.Count; i++)
			{
				if ((bool)channelTable.Rows[i]["liveStatus"] == true && (bool)channelTable.Rows[i]["appStat"] == false)
				{
					ExecuteAplication(channelTable.Rows[i]["channelID"].ToString() , channelTable.Rows[i]["liveUrl"].ToString());
					channelTable.Rows[i]["appStat"] = true;
				}
			}

			this.statusSetToDataGridView();

			logger.Debug("\r\nafter");
			loggingLiveData();
		}

		public void ExecuteAplication(string channelID, string liveUrl)
		{
			logger.Trace("start");

			for (int i = 0; i < channelTable.Rows.Count; i++)
			{
				if (channelID.Equals(channelTable.Rows[i]["channelID"].ToString()))
				{
					channelTable.Rows[i]["appStat"] = true;
				}
			}

			//Processオブジェクトを作成する
			ProcessExe p = new ProcessExe();

			//起動するファイルを指定する
			p.StartInfo.FileName = "notepad.exe";
			
			//引数
			p.StartInfo.Arguments = "notepad.exe";

			//イベントハンドラがフォームを作成したスレッドで実行されるようにする
			p.SynchronizingObject = this;

			//イベントハンドラの追加
			p.Exited += new EventHandler(p_Exited);
			p.EventArgs = channelID.ToString();
			
			//プロセスが終了したときに Exited イベントを発生させる
			p.EnableRaisingEvents = true;
			
			//起動する
			p.Start();
		}

		private void p_Exited(object sender, EventArgs e)
		{
			//プロセスが終了したときに実行される
			logger.Trace("start");

			ProcessExe p = (ProcessExe)sender;
			for(int i = 0; i < channelTable.Rows.Count; i++)
			{
				if(p.EventArgs.ToString().Equals(channelTable.Rows[i]["channelID"].ToString()))
				{
					channelTable.Rows[i]["appStat"] = false;
					channelTable.Rows[i]["liveStatus"] = false;
					channelTable.Rows[i]["liveEndTime"] = DateTime.Now;
				}
			}
		}

		private void statusSetToDataGridView()
		{
			//datatableをdatagridviewに反映させる
			for (int i = 0; i < dataGridView.Rows.Count; i++)
			{
				for (int j = 0; j < channelTable.Rows.Count; j++)
				{
					if (dataGridView.Rows[i].Cells["dgvChannelID"].Value.ToString().Equals(channelTable.Rows[j]["channelID"].ToString()))
					{
						if (true.Equals(channelTable.Rows[j]["liveStatus"]))
						{
							dataGridView.Rows[i].Cells["dgvStatus"].Value = "配信中";
						}
						else
						{
							dataGridView.Rows[i].Cells["dgvStatus"].Value = "　-　";
						}
					}
				}
			}
		}

		private void loggingLiveData()
		{
			foreach (DataRow row in channelTable.Rows)
			{
				logger.Info("Name          : " + row["channelName"].ToString());
				logger.Info("LiveStatus    : " + row["liveStatus"].ToString());
				logger.Info("AppStatus     : " + row["appStat"].ToString());
			}
		}
	}
}
