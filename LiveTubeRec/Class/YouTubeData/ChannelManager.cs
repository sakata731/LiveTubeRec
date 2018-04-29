using NLog;
using System;
using System.Collections.Generic;
using System.Data;
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

		public ChannelManager(string iniFilePath)
		{
			IniFile ini = new IniFile(iniFilePath);
			this._schedule = new Schedule(iniFilePath);
			this._youTubeDataProvider = new YouTubeDataProvider(ini["API", "key"]);
		}

		public DataRow GetPreparedDataRow(string channelID, DataTable table)
		{
			logger.Debug("start");

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

		//現時刻がスケージュールされていればテーブルを更新する
		public void UpdateChanelData(ref DataTable table)
		{
			logger.Debug("checking start");

			for (int i = 0; i < table.Rows.Count; i++)
			{
				if ((bool)table.Rows[i]["appStat"] != true)
				{
					DateTime dt = DateTime.Now;
					int[] schedule = _schedule.scheduleList[dt.Hour];
					if (0 != schedule.Length
						&& (-1 == schedule[0] || 0 <= Array.IndexOf(schedule, dt.Minute))) //indexof 引数が要素の中で何番目にあるかを返す
					{
						Dictionary<string, object> dic = this.CheckLiveStatus(table.Rows[i]["channelID"].ToString());

						table.Rows[i]["liveStatus"] = dic["liveStatus"];
						if ((bool)dic["liveStatus"])
						{
							table.Rows[i]["liveID"] = dic["liveID"];
							table.Rows[i]["liveTitle"] = dic["liveTitle"];
							table.Rows[i]["liveUrl"] = dic["liveUrl"];

							table.Rows[i]["liveStartTime"] = dic["liveStartTime"];
						}

						table.Rows[i]["liveLastRequestTime"] = dic["liveLastRequestTime"];

						logger.Debug("row changed");
					}
				}
			}
		}

		public void UpdateChanelData(DataRow row)
		{
			Dictionary<string, object> dic = this.CheckLiveStatus(row["channelID"].ToString());

			row["liveStatus"] = dic["liveStatus"];
			if ((bool)dic["liveStatus"])
			{
				row["liveID"] = dic["liveID"];
				row["liveTitle"] = dic["liveTitle"];
				row["liveUrl"] = dic["liveUrl"];
				row["liveStartTime"] = dic["liveStartTime"];
			}

			row["liveLastRequestTime"] = dic["liveLastRequestTime"];
		}


		private Dictionary<string, object> CheckLiveStatus(string channelID)
		{
			logger.Debug("start");

			DateTime dt = DateTime.Now;

			Dictionary<string, object> dic = _youTubeDataProvider.RequestLiveData(channelID);
			if ((bool)dic["liveStatus"])
			{
				dic.Add("liveStartTime", dt);
			}

			dic.Add("liveLastRequestTime", dt);

			return dic;
		}
	}
}
