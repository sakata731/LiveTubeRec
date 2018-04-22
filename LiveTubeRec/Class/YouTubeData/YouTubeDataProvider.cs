using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;


namespace LiveTubeRec
{
	//YouTubeAPIでデータを取得し、LiveDataで返却するクラス
	public class YouTubeDataProvider
	{
		private YouTubeService _youtubeService;

		public YouTubeDataProvider(string apiKey)
		{
			_youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = apiKey
			});
		}

		//チャンネルから生放送の情報を取得します
		public LiveData RequestLiveData(string channelID)
		{
			LiveData liveData;

			//検索条件の設定
			var searchListRequest = _youtubeService.Search.List("id,snippet");
			searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
			searchListRequest.Type = "video";
			searchListRequest.Fields = "items(id/videoId,snippet(title,channelTitle))";
			searchListRequest.ChannelId = channelID;

			//APIの発行
			var searchListResponse = searchListRequest.Execute();

			//データ数のカウント
			if (searchListResponse.Items.Count > 0)
			{
				liveData = new LiveData();

				//データの整形
				liveData.ID = searchListResponse.Items[0].Id.VideoId;
				liveData.Title = searchListResponse.Items[0].Snippet.Title;
				liveData.URL = @"https://www.youtube.com/watch?v=" + liveData.ID;
			}else
			{
				liveData = null;
			}

			return liveData;
		}
	}
}
