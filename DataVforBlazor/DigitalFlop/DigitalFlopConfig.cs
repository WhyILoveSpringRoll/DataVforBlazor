using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class DigitalFlopConfig
    {
        public List<int> number { get; set; } = new List<int>();
        public string content { get; set; } = "";
        public int toFixed { get; set; } = 0;
        public string textAlign { get; set; } = "center";// 'center' | 'left' | 'right'
        public int rowGap { get; set; } = 0;
        public int fontSize { get; set; } = 30;
        public string fill { get; set; } = "#3de7c9";
        public string formatter { get; set; }
        public string animationCurve { get; set; } = "easeOutCubic";
        public int animationFrame { get; set; } = 50;
    }
}
