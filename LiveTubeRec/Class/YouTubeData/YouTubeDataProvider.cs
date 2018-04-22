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
		public Dictionary<string,string> RequestLiveData(string channelID)
		{
			Dictionary<string, string> dictionary;

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
				dictionary = new Dictionary<string, string>();

				//データの整形
				dictionary.Add("liveID", searchListResponse.Items[0].Id.VideoId);
				dictionary.Add("liveTitle", searchListResponse.Items[0].Snippet.Title);
				dictionary.Add("liveUrl", "https://www.youtube.com/watch?v=" + searchListResponse.Items[0].Id.VideoId);
			}else
			{
				dictionary = null;
			}

			return dictionary;
		}

		//チャンネルIDからチャンネルの情報を取得します
		public Dictionary<string, string> RequestChannelData(string channelID)
		{
			Dictionary<string, string> dictionary;

			//検索条件の設定
			var searchListRequest = _youtubeService.Search.List("snippet");
			searchListRequest.Type = "channel";
			searchListRequest.Fields = "items(snippet/title,snippet/thumbnails/default/url)";
			searchListRequest.ChannelId = channelID;

			//APIの発行
			var searchListResponse = searchListRequest.Execute();

			//データ数のカウント
			if (searchListResponse.Items.Count > 0)
			{
				dictionary = new Dictionary<string, string>();

				//データの整形
				dictionary.Add("channelName", searchListResponse.Items[0].Snippet.Title);
				dictionary.Add("thumbnail", searchListResponse.Items[0].Snippet.Thumbnails.Default__.Url);
			}
			else
			{
				dictionary = null;
			}

			return dictionary;
		}

	}
}
