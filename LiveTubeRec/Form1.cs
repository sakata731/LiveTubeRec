using System;
using System.Windows.Forms;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace LiveTubeRec
{
	public partial class Form1 : Form
	{
		private string _APIKEY = "AIzaSyDlNaoWfOHxy5PsL7QPYNN5oXOoRVp6z3I";

		public Form1()
		{
			InitializeComponent();
			timer.Start();
		}

		private void buttonInsert_Click(object sender, EventArgs e)
		{
			string urlType = "";
			string urlID = "";

			if (chackURL(textBoxChannelID.Text, ref urlType, ref urlID))
			{
				switch (urlType)
				{
					case "channel":
						if (!hasChannelID(urlID))
						{
							YoutubeData youtubedata = new YoutubeData();
							youtubedata.channelID = urlID;

							setYoutubeData(ref youtubedata);

							DataRow row = dataSet.Tables[0].NewRow();
							row["channelName"] = youtubedata.channelName;
							row["channelID"] = youtubedata.channelID;
							row["liveID"] = youtubedata.liveID;
							row["liveTitle"] = youtubedata.liveTitle;
							row["liveURL"] = youtubedata.liveURL;
							row["liveStartDate"] = youtubedata.liveStartDate;
							row["status"] = youtubedata.status;
							row["requestLastDate"] = youtubedata.requestLastDate;

							dataSet.Tables[0].Rows.Add(row);
						}
						else
						{
							writeLog("すでに登録されています。");
						}
						break;
					default:
						break;
				}
			}
			else
			{
				writeLog("入力に誤りがあります。");
			}

			textBoxChannelID.Text = "";
		}


		// テキストボックスへログ出力
		// 引数：
		//  logText : ログとして書き出すテキスト
		void writeLog(String logText)
		{
			textBox_Log.SelectionStart = textBox_Log.Text.Length;
			textBox_Log.SelectionLength = 0;
			textBox_Log.SelectedText = "[" + System.DateTime.Now.ToString() + "] " + logText + "\r\n";
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				DataRow row = dataSet.Tables[0].Rows[i];

				YoutubeData youtubedata = new YoutubeData();
				youtubedata.channelName = row["channelName"].ToString();
				youtubedata.channelID = row["channelID"].ToString();
				youtubedata.liveID = row["liveID"].ToString();
				youtubedata.liveTitle = row["liveTitle"].ToString();
				youtubedata.liveURL = row["liveURL"].ToString();
				youtubedata.liveStartDate = (DateTime)row["liveStartDate"];
				youtubedata.status = (bool)row["status"];
				youtubedata.requestLastDate = (DateTime)row["requestLastDate"];

				setYoutubeData(ref youtubedata);

				dataSet.Tables[0].Rows[i]["channelName"] = youtubedata.channelName;
				dataSet.Tables[0].Rows[i]["channelID"] = youtubedata.channelID;
				dataSet.Tables[0].Rows[i]["liveID"] = youtubedata.liveID;
				dataSet.Tables[0].Rows[i]["liveTitle"] = youtubedata.liveTitle;
				dataSet.Tables[0].Rows[i]["liveURL"] = youtubedata.liveURL;
				dataSet.Tables[0].Rows[i]["liveStartDate"] = youtubedata.liveStartDate;
				dataSet.Tables[0].Rows[i]["status"] = youtubedata.status;
				dataSet.Tables[0].Rows[i]["requestLastDate"] = youtubedata.requestLastDate;
			}
		}

		private void requestLiveID(ref YoutubeData inputYoutubeData)
		{
			var youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = _APIKEY
			});

			var searchListRequest = youtubeService.Search.List("id,snippet");
			searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
			searchListRequest.Type = "video";
			searchListRequest.Fields = "items(id/videoId,snippet(title,channelTitle))";
			searchListRequest.ChannelId = inputYoutubeData.channelID;

			var searchListResponse = searchListRequest.Execute();

			if (searchListResponse.Items.Count > 0)
			{
				inputYoutubeData.liveID = searchListResponse.Items[0].Id.VideoId;
				inputYoutubeData.liveTitle = searchListResponse.Items[0].Snippet.Title;
				inputYoutubeData.channelName = searchListResponse.Items[0].Snippet.ChannelTitle;
				inputYoutubeData.liveURL = @"https://www.youtube.com/watch?v=" + inputYoutubeData.liveID;

			}
		}

		private void setYoutubeData(ref YoutubeData youtubedata)
		{
			youtubedata.requestLastDate = System.DateTime.Now;

			writeLog("[INFO] " + "チャンネルID : " + youtubedata.channelID);
			requestLiveID(ref youtubedata);

			if (youtubedata.liveID != null && !"".Equals(youtubedata.liveID))
			{
				if (!youtubedata.status)
				{
					try
					{
						ProcessStartInfo info = new ProcessStartInfo();
						info.FileName = "youtube-dl.exe";
						info.WorkingDirectory = Directory.GetCurrentDirectory() + "\\youtubeDL";
						info.Arguments = youtubedata.liveURL;
						Process.Start(info);
					}
					catch (Exception e)
					{
						writeLog(e.Message);
					}

					youtubedata.liveStartDate = System.DateTime.Now;
				}

				youtubedata.status = true;
			}
			else
			{
				if (youtubedata.status)
				{
					youtubedata.liveEndDate = System.DateTime.Now;
				}

				youtubedata.liveID = null;
				youtubedata.liveURL = null;
				youtubedata.status = false;
			}
		}

		private bool chackURL(string inputURL, ref string rtnType, ref string rtnID)
		{
			string expression = "(?<type>channel)/(?<id>.*?)(&|$|/)";

			Regex reg = new Regex(expression);
			Match match = reg.Match(inputURL);
			bool rtn = match.Success;
			if (rtn == true)
			{
				rtnType = match.Groups["type"].Value;
				rtnID = match.Groups["id"].Value;
			}

			return rtn;
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
		private bool hasChannelID(string inputCannelID)
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
			dataSet.WriteXml(@".\data\data.xml");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			dataSet.Clear();
			DirectoryUtils.SafeCreateDirectory(@".\data");

			if(System.IO.File.Exists(@".\data\data.xml"))
			{
				dataSet.ReadXml(@".\data\data.xml");
			}
			else
			{
				dataSet.WriteXml(@".\data\data.xml");
			}
		}
	}
}
