using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeRec
{
	/// <summary>
	/// youtubeAPIから返ってきた値を格納するクラス
	/// </summary>
	class YoutubeData
	{
		public string	channelName		{ get; set; }	//チェンネル名
		public string	channelID		{ get; set; }	//チャンネルID
		public string	liveID			{ get; set; }	//ライブID
		public string	liveURL			{ get; set; }	//ライブURL
		public string	liveTitle		{ get; set; }	//ライブタイトル
		public bool		status			{ get; set; }	//放送状況（true:放送中 false:未放送）
		public DateTime requestLastDate	{ get; set; }	//最終リクエスト日時
		public DateTime liveStartDate	{ get; set; }	//放送開始時間
		public DateTime liveEndDate		{ get; set; }	//放送終了時間

		public YoutubeData()
		{
			status = false;
		}
	}
}
