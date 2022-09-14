using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class PercentPondConfig
    {
        public double value { get; set; } = 0;
        public List<string> colors { get; set; } = new List<string>() { "#3DE7C9", "#00BAFF" };

        public int borderWidth { get; set; } = 3;
        public int borderGap { get; set; } = 3;
        public List<int> lineDash { get; set; } = new List<int> { 5, 1 };
        public string textColor { get; set; } = "#fff";
        public int borderRadius { get; set; } = 5;
        public bool showValue { get; set; } = true;
        public bool localGradient { get; set; } = false;
        public string formatter = "{value}%";
    }
}
