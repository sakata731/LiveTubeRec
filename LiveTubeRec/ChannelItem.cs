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

		public string	channelName		{ get; set; }	//チェンネル名
		public string	channelID		{ get; set; }   //チャンネルID
		public Schedule schedule		{ get; set; }	//スケジュール
		public LiveData _liveData { get; set; }
		private YouTubeDataProvider youtubeDataProvider;

		//初期化 初期化
		public bool Initialize(string channelURL, YouTubeDataProvider youtubeDataProvider, Schedule schedule)
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

		//URLからチェンネルIDを取得 取得できなければnullを返す
		private string SetChannelIDFromURL(string inputURL)
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
			if (0 <= Array.IndexOf(schedule.scheduleList[dt.Hour], dt.Minute))
			{
				this._liveData = youtubeDataProvider.RequestLiveInformation(this.channelID);
				this._liveData.lastRequestDate = dt;
				
				_LogingLiveData();
			}
		}

		private void _LogingLiveData()
		{
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.status" + _liveData.status);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.id" + _liveData.id);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.url" + _liveData.url);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.title" + _liveData.title);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.startDate" + _liveData.startDate);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.endDate" + _liveData.endDate);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.lastRequestDate" + _liveData.lastRequestDate);
			logger.Debug("[" + this.GetType().Name + "][" + MethodBase.GetCurrentMethod().Name + "]" + "LiveData.nextRequestDate" + _liveData.nextRequestDate);
		}
	}
}
