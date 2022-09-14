using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class WaterLevelPondConfig
    {
        public List<double> data = new List<double>();
        public int shape { get; set; } = WaterLevelPondShape.Rect;

        public List<string> colors { get; set; } = new List<string> { "#00BAFF", "#3DE7C9" };

        public int waveNum { get; set; } = 3;
        public int waveHeight { get; set; } = 40;
        public double waveOpacity { get; set; } = 0.4;
        public string formatter = "";
    }
    public readonly struct WaterLevelPondShape
    {
        public static int Rect = 0;
        public static int RoundRect = 1;
        public static int Round = 2;
    }
}
