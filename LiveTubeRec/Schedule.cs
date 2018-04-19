using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeRec
{
	public class Schedule
	{
		public int[] h0 { get; set; }
		public int[] h1 { get; set; }
		public int[] h2 { get; set; }
		public int[] h3 { get; set; }
		public int[] h4 { get; set; }
		public int[] h5 { get; set; }
		public int[] h6 { get; set; }
		public int[] h7 { get; set; }
		public int[] h8 { get; set; }
		public int[] h9 { get; set; }
		public int[] h10 { get; set; }
		public int[] h11 { get; set; }
		public int[] h12 { get; set; }
		public int[] h13 { get; set; }
		public int[] h14 { get; set; }
		public int[] h15 { get; set; }
		public int[] h16 { get; set; }
		public int[] h17 { get; set; }
		public int[] h18 { get; set; }
		public int[] h19 { get; set; }
		public int[] h20 { get; set; }
		public int[] h21 { get; set; }
		public int[] h22 { get; set; }
		public int[] h23 { get; set; }

		public List<int[]> scheduleList { get; set; }

		IniFile ini;

		public Schedule(string filePath)
		{
			//ファイルを指定して初期化
			ini = new IniFile(filePath);
		}

		public void ReadSchedule()
		{
			scheduleList = new List<int[]>
			{   h0, h1, h2, h3, h4, h5,
				h6, h7, h8, h9, h10, h11,
				h12, h13,h14, h15, h16, h17,
				h18, h19, h20,h21, h22, h23,
			};

			//読み込み
			for(int i = 0; i < scheduleList.Count; i++)
			{
				String[] times = ini["Schedule", "h" + i.ToString()].Split(',');

				if (times != null && !"".Equals(times[0]))
				{
					scheduleList[i] = times.Select(s => int.Parse(s)).ToArray();
				}
			}
		}
	}
}
