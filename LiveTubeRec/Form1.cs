using System;
using System.Windows.Forms;
using NLog;

namespace LiveTubeRec
{
	public partial class Form1 : Form
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
		private YouTubeDataProvider _youtubeGateway;
		private Schedule _schedule;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//apiキーの初期設定
			IniFile ini = new IniFile(@".\config\config.ini");
			_youtubeGateway = new YouTubeDataProvider(ini["API", "key"]);

			//リクエストスケジュールの初期化
			_schedule = new Schedule(@".\config\schedule.ini");
			_schedule.ReadSchedule();

			//データセットの初期化
			liveTubeDataSet.Clear();
			DirectoryUtils.SafeCreateDirectory(@".\data");

			if (System.IO.File.Exists(@".\data\data.xml"))
			{
				liveTubeDataSet.ReadXml(@".\data\data.xml");
			}
			else
			{
				liveTubeDataSet.WriteXml(@".\data\data.xml");
			}


		}


		//<summary>
		// 追加ボタン
		//</summary>
		private void buttonInsert_Click(object sender, EventArgs e)
		{
			textBoxChannelID.Text = "";
		}





		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			dataGridView.Rows.Remove(dataGridView.CurrentRow);
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

		//リストにチャンネルIDが登録されているかをチェック
		// 登録済み:true 未登録:false
		private bool HasChannelID(string inputCannelID)
		{
			bool rtn = false;
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				// 編集中の行どうしでないとき
				if (row.Cells["channelID"].Value != null && row.Cells["channelID"].Value.Equals(inputCannelID))
				{
					rtn = true;
				}
			}

			return rtn;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			DirectoryUtils.SafeCreateDirectory(@".\data");
			liveTubeDataSet.WriteXml(@".\data\data.xml");
		}

	}
}
