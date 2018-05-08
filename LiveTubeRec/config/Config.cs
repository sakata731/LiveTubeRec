using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeRec
{
	class Config
	{
		public String ApiKey { get; set; }
		public String YTDLPath { get; set; }
		public Config(string iniPath)
		{
			//ファイルを指定して初期化
			IniFile ini = new IniFile(iniPath);

			this.ApiKey = ini["API", "key"];
			this.YTDLPath = ini["YouTubeDL", "path"];
		}
	}
}
