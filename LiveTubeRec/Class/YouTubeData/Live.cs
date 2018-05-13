using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeReport
{
	public class Live
	{
		public string ID { get; set; }  //ライブID
		public string URL { get; set; } //ライブURL
		public string Title { get; set; }  //ライブタイトル
		public bool Status { get; set; }   //放送状況（true:放送中 false:未放送）
		public DateTime StartTime { get; set; } //放送開始時間
		public DateTime EndTime { get; set; } //放送終了時間
		public DateTime NextRequestTime { get; set; } //次回のAPIリクエスト時間
		public DateTime LastRequestTime { get; set; }  //最終リクエスト日時 未実装
	}
}
