using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class ConicalColumnChartConfig
    {
        public int fontsize { get; set; } = 12;
        public int imgSideLength { get; set; } = 30;

        public string columnColor { get; set; } = "rgba(0, 194, 255, 0.4)";

        public string textColor { get; set; } = "#fff";
        public bool showValue { get; set; } = false;

        public List<string> img { get; set; } = new List<string>();

        public Dictionary<string, double> data =new Dictionary<string, double>();
    }
}
