using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeReport
{
	public class Schedule
	{
		public List<int[]> scheduleList { get; set; }

		IniFile ini;

		public Schedule(string filePath)
		{
			//ファイルを指定して初期化
			ini = new IniFile(filePath);
			this.ReadSchedule();
		}

		public void ReadSchedule()
		{
			scheduleList = new List<int[]>();

			//読み込み
			for(int i = 0; i < 24; i++)
			{
				String[] times = ini["Schedule", "h" + i.ToString()].Split(',');

				if (times != null && !"".Equals(times[0]))
				{
					int[] tmp = times.Select(s => int.Parse(s)).ToArray();
					scheduleList.Add(tmp);
				}
				else
				{
					scheduleList.Add(new int[0]);
				}
			}
		}
	}
}
