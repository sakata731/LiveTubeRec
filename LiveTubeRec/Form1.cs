using System;
using System.Windows.Forms;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Linq;

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
            dataGridView.Rows.Add("", textBoxChannelID.Text);
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
                string liveID = requestLiveID(row.Cells["channelID"].Value.ToString());

                if(!"".Equals(liveID))
                {

                    writeLog("debug : " + liveID);

                    row.Cells["liveID"].Value = liveID;
                    row.Cells["status"].Value = "配信中";
                    row.Cells["liveURL"].Value = @"https://www.youtube.com/watch?v=" + liveID;
                }
                else
                {
                    row.Cells["liveID"].Value = "";
                    row.Cells["status"].Value = "-";
                    row.Cells["liveURL"].Value = "";
                }
            }
        }

        private string requestLiveID(string channelID)
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

            string retLiveID = "";

            if(searchListResponse.Items.Count > 0)
            {
                retLiveID = searchListResponse.Items[0].Id.VideoId;
            }

            return retLiveID;
        }
    }
}
