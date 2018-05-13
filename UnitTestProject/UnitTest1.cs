using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiveTubeReport;
using System.Threading;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			string config = @".\config\config.ini";

			YouTubeDataProvider ydp = new YouTubeDataProvider((new IniFile(config)["API","key"]));
			Schedule schedule = new Schedule(config);

			Channel item = new Channel();
			item.Initialize("https://www.youtube.com/channel/UC87o3e14Vh9lQ6BhZXqC6GQ", ydp, schedule);
			item.StartMoniterLiveStatus();

			Thread.Sleep(60000);

			Assert.AreEqual(DateTime.Now, item.LiveData.LastRequestTime); // 実行結果の確認
		}
	}
}
