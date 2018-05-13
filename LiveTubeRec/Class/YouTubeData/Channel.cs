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

namespace LiveTubeReport
{
	/// <summary>
	/// チャンネルの情報を保持するクラス
	/// </summary>
	public class Channel
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		public string	Name				{ get; set; }  //チェンネル名
		public string	ID					{ get; set; }   //チャンネルID
		public string	Thumbnail			{ get; set; }  //サムネイル
		public DateTime	AddDate				{ get; set; }
		public Schedule	Schedule			{ get; set; }  //スケジュール 未使用
		public Live		Live				{ get; set; }

		public void LogingChannelItem()
		{
			logger.Debug("Name			: " + this.Name);
			logger.Debug("ChannelID		: " + this.ID);
			logger.Debug("Thumbnail		: " + this.Thumbnail);
			logger.Debug("AddDate		: " + this.AddDate);
			logger.Debug("LiveID		: " + this.Live.ID);
			logger.Debug("LiveURL		: " + this.Live.URL);
			logger.Debug("LiveTitle		: " + this.Live.Title);
			logger.Debug("LiveStatus	: " + this.Live.Status);
			logger.Debug("LiveStartTime	: " + this.Live.StartTime);
			logger.Debug("LiveEndTime	: " + this.Live.EndTime);
			logger.Debug("LiveNextRequestTime	: " + this.Live.NextRequestTime);
			logger.Debug("LiveLastRequestTime	: " + this.Live.LastRequestTime);
		}
	}
}
