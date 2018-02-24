using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTubeRec
{
    class YoutubeData
    {
        public string   channelName     { get; set; }
        public string   channelID       { get; set; }
        public string   liveID          { get; set; }
		public string	liveURL			{ get; set; }
        public string   liveTitle       { get; set; }
		public bool		status			{ get; set; }
        public DateTime requestLastDate { get; set; }
        public DateTime liveStartDate   { get; set; }
        public DateTime liveEndDate     { get; set; }

		public YoutubeData()
		{
			status = false;
		}
    }
}
