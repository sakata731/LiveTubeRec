using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeRec
{
	public class LiveData
	{
		public string id { get; set; }  //ライブID
		public string url { get; set; } //ライブURL
		public string title { get; set; }   //ライブタイトル
		public bool status { get; set; } = false;   //放送状況（true:放送中 false:未放送）
		public DateTime startDate { get; set; } //放送開始時間
		public DateTime endDate { get; set; }   //放送終了時間
		public DateTime nextRequestDate { get; set; }  //次回のAPIリクエスト時間
		public DateTime lastRequestDate { get; set; }   //最終リクエスト日時
	}
}
