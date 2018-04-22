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
	/// <summary>
	/// チャンネルの情報を保持するクラス
	/// </summary>
	public class ChannelItem
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		public string	ChannelName		{ get; set; }	//チェンネル名
		public string	ChannelID		{ get; set; }   //チャンネルID
		public LiveData LiveData { get; set; }
		public Schedule Schedule { private get; set; }  //スケジュール
		public YouTubeDataProvider YoutubeDataProvider{ private get; set; }

		public ChannelItem()
		{
			LiveData = new LiveData();
		}

		/*
		//初期化 初期化
		public bool Initialize(string channelID, YouTubeDataProvider youtubeDataProvider, Schedule schedule)
		{
			logger.Debug("[" + this.GetType().Name + "]["+ MethodBase.GetCurrentMethod().Name + "]" + "Start");

			bool rtn;

			this.youtubeDataProvider = youtubeDataProvider;
			this.schedule = schedule;

			if(this.SetChannelIDFromURL(channelURL) != null)
			{
				_liveData = new LiveData();
				rtn = true;
			}
			else
			{
				rtn = false;
			}

			_LogingLiveData();
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "End");
			return rtn;
		}
		*/
		//ライブ情報のモニタリング開始
		public void StartMoniterLiveStatus()
		{
			System.Diagnostics.Debug.WriteLine("StartMoniterLiveStatus");
			Timer timer = new Timer();
			timer.Elapsed += new ElapsedEventHandler(_CheckLiveStatus);
			timer.Interval = 30000 /*msec*/;

			timer.Start();
		}

		//現時刻がスケージュールされていればYouTuveDataProviderを呼び出しライブデータを更新する
		private void _CheckLiveStatus(object sender, ElapsedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("_CheckLiveStatus");

			DateTime dt = DateTime.Now;
			if (0 <= Array.IndexOf(Schedule.scheduleList[dt.Hour], dt.Minute))
			{
				this.LiveData = YoutubeDataProvider.RequestLiveData(this.ChannelID);
				this.LiveData.LastRequestTime = dt;
				
				_LogingLiveData();
			}
		}

		private void _LogingLiveData()
		{
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.status" + LiveData.Status);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.id" + LiveData.ID);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.url" + LiveData.URL);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.title" + LiveData.Title);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.startDate" + LiveData.StartTime);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.endDate" + LiveData.EndTime);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.lastRequestDate" + LiveData.LastRequestTime);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.nextRequestDate" + LiveData.NextRequestTime);
		}
	}
}
