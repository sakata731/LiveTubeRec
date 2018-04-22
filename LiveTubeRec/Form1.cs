using System;
using System.Windows.Forms;
using NLog;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;

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
			DirectoryUtils.SafeCreateDirectory(@".\data\image");

			if (System.IO.File.Exists(@".\data\data.xml"))
			{
				liveTubeDataSet.ReadXml(@".\data\data.xml");

				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					DataRow[] rows = liveTubeDataSet.Tables[0].Select("ChannelID = " + "'" + row.Cells["dgvChannelID"].Value.ToString() + "'");

					row.Cells["dgvThumbnail"].Value = new Bitmap(rows[0]["ThumbnailImage"].ToString());

					if (System.Convert.ToBoolean(rows[0]["Status"].ToString()))
					{
						row.Cells["dgvStatus"].Value = "配信中";
					}
					else
					{
						row.Cells["dgvStatus"].Value = "";
					}
				}


				logger.Info("データの読み込みに成功しました。");
			}
			else
			{
				liveTubeDataSet.WriteXml(@".\data\data.xml");
			}

			_ChannelManager = new ChannelManager(@".\config\config.ini");

			int rtn = _ChannelManager.CreateChannelItemList(liveTubeDataSet.Tables[0]);


			logger.Debug("ChannelItem Count : " + rtn.ToString());
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

			string channelID = this.GetChannelID(textBoxChannelID.Text);
			if (!"".Equals(channelID) && !HasChannelID(channelID))
			{
				ChannelItem item = _ChannelManager.AddChannelItem(channelID);

				DataRow row = this.CreateChannelRow(
					item.Id,
					item.Name,
					item.Thumbnail,
					item.LiveData.ID,
					item.LiveData.Title,
					item.LiveData.URL,
					item.LiveData.Status,
					item.LiveData.StartTime,
					item.LiveData.EndTime,
					item.LiveData.LastRequestTime,
					item.LiveData.NextRequestTime,
					item.AddDate
				);

				channelTable.Rows.Add(row);
			}
			else
			{
				logger.Error("入力したURLが正しくないか、一覧に存在しているため追加できません。");
			}

			textBoxChannelID.Text = "";
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			DataGridViewRow row = dataGridView.CurrentRow;

			_ChannelManager.RemoveChannelItem(row.Cells["channelID"].Value.ToString());
			dataGridView.Rows.Remove(row);
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

		//URLからチェンネルIDを取得 取得できなければ空文字を返す
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

		private DataRow CreateChannelRow(
			string channelID,
			string channelName,
			string thumbnail,
			string liveID,
			string liveTitle,
			string liveUrl,
			bool status,
			DateTime liveStartTime,
			DateTime liveEndTime,
			DateTime liveLastRequestTime,
			DateTime liveNextRequestTime,
			DateTime addDate
			)
		{
			DataRow row = channelTable.NewRow();

			row["ChannelID"] = channelID;
			row["ChannelName"] = channelName;
			row["Thumbnail"] = thumbnail;
			row["LiveID"] = liveID;
			row["LiveTitle"] = liveTitle;
			row["LiveUrl"] = liveUrl;
			row["Status"] = status;
			row["LiveStartTime"] = liveStartTime;
			row["LiveEndTime"] = liveEndTime;
			row["LastRequestTime"] = liveLastRequestTime;
			row["NextRequestTime"] = liveNextRequestTime;
			row["AddDate"] = addDate;

			string fileName = @".\data\image\" + channelID + ".png";
			row["ThumbnailImage"] = fileName;
			if (!System.IO.File.Exists(fileName))
			{
				Bitmap bmp = new Bitmap(Utils.loadImageFromURL(thumbnail));
				bmp.Save(@".\data\image\" + channelID + ".png", System.Drawing.Imaging.ImageFormat.Png);
			}

			return row;
		}
	}
}
