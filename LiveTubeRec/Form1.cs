using System;
using System.Windows.Forms;
using NLog;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;

namespace LiveTubeRec
{
	public partial class Form1 : Form
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private static ChannelManager _ChannelManager;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
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

			_ChannelManager = new ChannelManager(@".\config\config.ini");
			int rtn = _ChannelManager.CreateChannelItemList(liveTubeDataSet.Tables[0]);


		}


		//<summary>
		// 追加ボタン
		//</summary>
		private void buttonInsert_Click(object sender, EventArgs e)
		{
			string inputText = textBoxChannelID.Text;

			if ("".Equals(inputText)) return;

			string channelID = this.GetChannelID(textBoxChannelID.Text);
			if (!"".Equals(channelID) && !HasChannelID(channelID))
			{
				_ChannelManager.AddChannelItem(channelID);

				DataTable table = liveTubeDataSet.Tables["ChannelTable"];
				DataRow row = table.NewRow();
				row["channelID"] = _ChannelManager.GetChannelItem(channelID).ChannelID;

				table.Rows.Add(row);
			}
			else
			{
				logger.Error("入力したURLが正しくないか、一覧に存在しているため追加できません。");
			}

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

		//URLからチェンネルIDを取得 取得できなければnullを返す
		private string GetChannelID(string inputURL)
		{
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "Start");

			string expression = "(?<type>channel)/(?<id>.*?)(&|$|/)";

			Regex reg = new Regex(expression);
			Match match = reg.Match(inputURL);
			bool rtn = match.Success;
			if (rtn == true)
			{
				//rtnType = match.Groups["type"].Value;
				return match.Groups["id"].Value;
			}
			else
			{
				return null;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			DirectoryUtils.SafeCreateDirectory(@".\data");
			liveTubeDataSet.WriteXml(@".\data\data.xml");
		}

	}
}
