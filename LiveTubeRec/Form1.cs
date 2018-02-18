using System;
using System.Windows.Forms;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

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

            if(chackURL(textBoxChannelID.Text, ref urlType, ref urlID))
            {
                int rowNum = -1;
                switch (urlType)
                {
                    case "channel":
                        if (!textBoxChannelID_Validating(urlID))
                        {
                            rowNum = dataGridView.Rows.Add("", urlID);
                        }
                        else
                        {
                            writeLog("すでに登録されています。");
                        }
                        break;
                    default:
                        break;
                }

                if(rowNum > 0)
                {
                    //setStatus(dataGridView.Rows[rowNum]);
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
            textBox_Log.SelectedText = "[" + System.DateTime.Now.ToString() + "]" + logText + "\r\n";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (var row in dataGridView.Rows.Cast<DataGridViewRow>())
            {
                setStatus(row);
            }
        }

        private YoutubeDataModel requestLiveID(string channelID)
        {
            writeLog("Call Method : requestLiveID");

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _APIKEY
            });

            var searchListRequest = youtubeService.Search.List("id");
            searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
            searchListRequest.Type = "video";
            searchListRequest.Fields = "items(id/videoId)";
            searchListRequest.ChannelId = channelID;

            var searchListResponse = searchListRequest.Execute();

            YoutubeDataModel youtubeDataModel = null;

            if (searchListResponse.Items.Count > 0)
            {
                youtubeDataModel = new YoutubeDataModel();
                youtubeDataModel.liveID = searchListResponse.Items[0].Id.VideoId;
                youtubeDataModel.liveTitle = searchListResponse.Items[0].Snippet.ChannelTitle;
            }

            return youtubeDataModel;
        }

        private void setStatus(DataGridViewRow row)
        {
            YoutubeDataModel youtubeDataModel = requestLiveID(row.Cells["channelID"].Value.ToString());

            if (!"".Equals(youtubeDataModel.liveID))
            {

                writeLog("debug : " + youtubeDataModel.liveID);

                row.Cells["liveURL"].Value = @"https://www.youtube.com/watch?v=" + youtubeDataModel.liveID;

                if (!"配信中".Equals(row.Cells["status"].Value))
                {
                    try
                    {
                       ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = "youtube-dl.exe";
                        info.WorkingDirectory = Directory.GetCurrentDirectory() + "\\youtubeDL";
                        info.Arguments = row.Cells["liveURL"].Value.ToString();
                        Process.Start(info);
                    }
                    catch(Exception e)
                    {
                        writeLog(e.Message);
                    }
                }

                row.Cells["liveID"].Value = youtubeDataModel.liveID;
                row.Cells["status"].Value = "配信中";            }
            else
            {
                row.Cells["liveID"].Value = "";
                row.Cells["status"].Value = "-";
                row.Cells["liveURL"].Value = "";
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

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            writeLog("dataGridView_MouseDown");
            if (e.Button == MouseButtons.Right)
            {
                dataGridView.ClearSelection();
            }
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            writeLog("dataGridView_CellMouseDown");
            if (e.Button == MouseButtons.Right)
            {
                dataGridView.ClearSelection();

                if (e.RowIndex > -1)
                {
                    dataGridView.Rows[e.RowIndex].Selected = true;
                }
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

        private bool textBoxChannelID_Validating(string inputCannelID)
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
    }
}
