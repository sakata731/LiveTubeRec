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


namespace LiveTubeReport
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

		/// <summary>
		///チャンネルから生放送の情報を取得します
		///keylist liveID, liveTitle, liveUrl
		/// </summary>
		public Dictionary<string, object> RequestLiveData(string channelID)
		{
			//検索条件の設定
			var searchListRequest = _youtubeService.Search.List("id,snippet");
			searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
			searchListRequest.Type = "video";
			searchListRequest.Fields = "items(id/videoId,snippet(title,channelTitle))";
			searchListRequest.ChannelId = channelID;

			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("liveStatus", false);

			//APIの発行
			var searchListResponse = searchListRequest.Execute();

			//データ数のカウント
			if (searchListResponse.Items.Count > 0)
			{
				//データの整形
				dictionary["liveStatus"] = true;
				dictionary.Add("liveID", searchListResponse.Items[0].Id.VideoId);
				dictionary.Add("liveTitle", searchListResponse.Items[0].Snippet.Title);
				dictionary.Add("liveUrl", "https://www.youtube.com/watch?v=" + searchListResponse.Items[0].Id.VideoId);
			}

			return dictionary;
		}

		//チャンネルIDからチャンネルの情報を取得します
		//keylist channelName, thumbnail
		public Dictionary<string, object> RequestChannelData(string channelID)
		{
			//検索条件の設定
			var searchListRequest = _youtubeService.Search.List("snippet");
			searchListRequest.Type = "channel";
			searchListRequest.Fields = "items(snippet/title,snippet/thumbnails/default/url)";
			searchListRequest.ChannelId = channelID;

			Dictionary<string, object> dictionary = new Dictionary<string, object>();

			//APIの発行
			var searchListResponse = searchListRequest.Execute();

			//データ数のカウント
			if (searchListResponse.Items.Count > 0)
			{
				//データの整形
				dictionary.Add("channelName", searchListResponse.Items[0].Snippet.Title);
				dictionary.Add("thumbnail", searchListResponse.Items[0].Snippet.Thumbnails.Default__.Url);
			}

			return dictionary;
		}
	}
}
