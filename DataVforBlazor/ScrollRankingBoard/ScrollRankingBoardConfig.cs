using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class ScrollRankingBoardConfig
    {
        public List<ScrollRankingBoardData> data { get; set; }
        public int rowNum { get; set; } = 5;
        public int waitTime { get; set; } = 2000;
        public string carousel { get; set; } = "single";
        public string unit { get; set; } = "";
        public bool sort { get; set; } = true;
    }

    public class ScrollRankingBoardData
    {
        public string name { get; set; }
        public double value { get; set; }
        internal double percent { get; set; }
        internal int ranking { get; set; }
        internal int scroll { get; set; }
        internal int height { get; set; }
    }
}
