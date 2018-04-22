using NLog;
using System;
using System.Collections.Generic;
using System.Data;
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

		private Dictionary<string,ChannelItem> _channelItems;
		private YouTubeDataProvider _youTubeDataProvider;
		private Schedule _schedule;

		public ChannelManager(string iniFilePath)
		{
			IniFile ini = new IniFile(iniFilePath);
			this._schedule = new Schedule(iniFilePath);

			this._youTubeDataProvider = new YouTubeDataProvider(ini["API", "key"]);
		}

		public ChannelItem GetChannelItem(string channelID)
		{
			return _channelItems[channelID];
		}

		//テーブルからチャンネル一覧のリストを生成
		public int CreateChannelItemList(DataTable table)
		{
			_channelItems = new Dictionary<string, ChannelItem>();

			foreach (DataRow row in table.Rows)
			{
				ChannelItem item = CreateChannelItem(
					row["ChannelID"].ToString(),
					row["ChannelName"].ToString(),
					row["Thumbnail"].ToString(),
					_schedule,
					new DateTime(),
					new DateTime(),
					new DateTime(),
					new DateTime(),
					DateTime.Parse(row["AddDate"].ToString())
				);

				//ライブ内容をログに吐き出す
				item.LogingChannelItem();

				_channelItems.Add(item.Id, item);
			}

			return _channelItems.Count;
		}

		//チャンネルIDからチャンネルアイテムを生成
		//チャンネルの存在確認の例外対策したい・・・
		public ChannelItem AddChannelItem(string channelID)
		{
			Dictionary<string, string> dic;
			dic = this._youTubeDataProvider.RequestChannelData(channelID);

			ChannelItem item = CreateChannelItem(
				channelID,
				dic["channelName"],
				dic["thumbnail"],
				_schedule,
				new DateTime(),
				new DateTime(),
				new DateTime(),
				new DateTime(),
				DateTime.Now
			);

			//ライブ内容をログに吐き出す
			item.LogingChannelItem();

			_channelItems.Add(item.Id, item);

			return item;
		}

		public void RemoveChannelItem(string channelID)
		{
			logger.Debug("Remove ChannelID : " + channelID);

			this._channelItems.Remove(channelID);
		}

		private ChannelItem CreateChannelItem(
			string channelID,
			string channelName,
			string thumbnail,
			Schedule schedule,
			DateTime liveStartTime,
			DateTime liveEndTime,
			DateTime liveLastRequestTime,
			DateTime liveNextRequestTime,
			DateTime addDate
		)
		{
			ChannelItem item = new ChannelItem();

			//チャンネル基本情報
			item.Id = channelID;
			item.Name = channelName;
			item.Thumbnail = thumbnail;
			item.Schedule = schedule;
			item.AddDate = addDate;

			//ライブ情報（初期化）
			LiveData live = new LiveData();
			live.ID = "";
			live.Status = false;
			live.Title = "";
			live.URL = "";
			live.StartTime = liveStartTime;
			live.EndTime = liveEndTime;
			live.LastRequestTime = liveLastRequestTime;
			live.NextRequestTime = liveNextRequestTime;

			item.LiveData = live;

			return item;
		}

		/*
		//現時刻がスケージュールされていればYouTuveDataProviderを呼び出しライブデータを更新する⇨マネージャクラスに移動
		private void _CheckLiveStatus(object sender, ElapsedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("_CheckLiveStatus");

			DateTime dt = DateTime.Now;
			if (0 <= Array.IndexOf(Schedule.scheduleList[dt.Hour], dt.Minute))
			{
				Dictionary<string, string> dic = YoutubeDataProvider.RequestLiveData(this.ChannelID);
				if (dic != null)
				{
					this.LiveData.ID = dic["id"];
					this.LiveData.Title = dic["title"];
					this.LiveData.URL = dic["url"];

					this.LiveData.StartTime = dt;
					this.LiveData.Status = true;
				}
				this.LiveData.LastRequestTime = dt;

				_LogingLiveData();
			}
		}
		
		//ライブ情報のモニタリング開始
		public void StartMoniterLiveStatus()
		{
			System.Diagnostics.Debug.WriteLine("StartMoniterLiveStatus");
			Timer timer = new Timer();
			timer.Elapsed += new ElapsedEventHandler(_CheckLiveStatus);
			timer.Interval = 30000; //msec

			timer.Start();
		}

	*/
	}
}
