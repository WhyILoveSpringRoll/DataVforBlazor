using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class CapsuleChartConfig
    {
        public string unit { get; set; }
        public bool showValue { get; set; } = false;

        public bool sort { get; set; } = false;

        public List<string> colors { get; set; } = new List<string> { "#37a2da", "#32c5e9", "#67e0e3", "#9fe6b8", "#ffdb5c", "#ff9f7f", "#fb7293" };

        public List<CapsuleData> data = new List<CapsuleData>();
    }

    public class CapsuleData
    {
        public string name { get; set; }
        public double value { get; set; }
        internal double length { get; set; }
    }
}
