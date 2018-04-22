using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiveTubeRec
{
	public class ChannelManager
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private Dictionary<string,ChannelItem> channelItems;
		private YouTubeDataProvider _youTubeDataProvider;
		private Schedule _schedule;

		public ChannelManager(string iniFilePath)
		{
			IniFile ini = new IniFile(iniFilePath);
			this._schedule = new Schedule(iniFilePath);

			this._youTubeDataProvider = new YouTubeDataProvider(ini["API", "key"]);
		}

		public int CreateChannelItemList(DataTable table)
		{
			channelItems = new Dictionary<string, ChannelItem>();

			foreach (DataRow row in table.Rows)
			{
				ChannelItem item = new ChannelItem();
				item.YoutubeDataProvider = _youTubeDataProvider;
				item.Schedule = _schedule;

				var a = row["liveStartDate"];

				item.ChannelID = row["channelID"].ToString();
				item.ChannelName = row["channelName"].ToString();
				item.LiveData.ID = row["liveID"].ToString();
				item.LiveData.Title = row["liveTitle"].ToString();
				item.LiveData.URL = row["liveURL"].ToString();
				item.LiveData.StartTime = (DateTime)row["liveStartDate"];
				item.LiveData.Status = (bool)row["status"];
				item.LiveData.LastRequestTime = (DateTime)row["requestLastDate"];

				channelItems.Add(item.ChannelID, item);
			}

			return channelItems.Count;
		}

		public void AddChannelItem(string channelID)
		{
			ChannelItem item = new ChannelItem();
			item.YoutubeDataProvider = _youTubeDataProvider;
			item.Schedule = _schedule;
			item.ChannelID = channelID;

			channelItems.Add(item.ChannelID, item);
		}

		public ChannelItem GetChannelItem(string channelID)
		{
			return channelItems[channelID];
		}
	}
}
