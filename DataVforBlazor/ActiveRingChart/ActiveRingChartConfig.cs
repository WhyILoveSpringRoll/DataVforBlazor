using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class ActiveRingChartConfig
    {
        public string radius { get; set; } = "50%";
        public string activeRadius { get; set; } = "55%";
        public List<ActiveRingData> data { get; set; } = new List<ActiveRingData>();
        public int lineWidth { get; set; } = 20;
        public int activeTimeGap { get; set; } = 3000;
        public List<string> color { get; set; } = new List<string> { "#000", "rgb(0, 0, 0)", "rgba(0, 0, 0, 1)", "red"};
        public bool showOriginValue { get; set; } = false;
        public int fontSize { get; set; } = 25;
        public string fill { get; set; } = "#3de7c9";
        public string animationCurve { get; set; } = "easeOutCubic";
        public int animationFrame { get; set; } = 50;
        public int toFixed { get; set; } = 0;
        public string digitalFlopUnit { get; set; } = "";
        public int activeIndex { get; set; } = 0;
    }
    public class ActiveRingData
    {
        public string name { get; set; }
        public double value { get; set; }
        internal List<double> radius { get; set; }
    }
}
