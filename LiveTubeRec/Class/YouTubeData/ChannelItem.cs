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

		public string Name { get; set; }  //チェンネル名
		public string Id { get; set; }   //チャンネルID
		public string Thumbnail { get; set; }  //サムネイル
		public DateTime AddDate { get; set; }
		public LiveData LiveData { get; set; }
		public Schedule Schedule { get; set; }  //スケジュール

		public void LogingChannelItem()
		{
			logger.Debug("ChannelItem.Name : " + this.Name);
			logger.Debug("ChannelItem.Id : " + this.Id);
			logger.Debug("ChannelItem.Thumbnail : " + this.Thumbnail);
			logger.Debug("LiveData.status : " + LiveData.Status);
			logger.Debug("LiveData.id : " + LiveData.ID);
			logger.Debug("LiveData.url : " + LiveData.URL);
			logger.Debug("LiveData.title : " + LiveData.Title);
			logger.Debug("LiveData.startDate : " + LiveData.StartTime);
			logger.Debug("LiveData.endDate : " + LiveData.EndTime);
			logger.Debug("LiveData.lastRequestDate : " + LiveData.LastRequestTime);
			logger.Debug("LiveData.nextRequestDate : " + LiveData.NextRequestTime);
			logger.Debug("ChannelItem.AddDate : " + this.AddDate);
		}
	}
}
