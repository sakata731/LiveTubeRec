using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeReport
{
	class Config
	{
		public string ApiKey { get; set; }
		public string YTDLPath { get; set; }
		public int ChannelNumMax { get; set; }
		public Config(string iniPath)
		{
			//ファイルを指定して初期化
			IniFile ini = new IniFile(iniPath);

			this.ApiKey = ini["API", "key"];
			this.YTDLPath = ini["YouTubeDL", "path"];
			this.ChannelNumMax = int.Parse( ini["Channel", "max"]);
		}
	}
}
