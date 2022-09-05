﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class FlylineChartConfig
    {
        public DvPoint centerPoint { get; set; } 

        public List<FlylineChartPoint> points = new List<FlylineChartPoint>();

        public List<FlylineChartFlyLine> lines = new List<FlylineChartFlyLine>();

        public FlylineChartHalo halo { get; set; }
        public FlylineChartText text { get; set; }
        public FlylineChartLine line { get; set; }
        public FlylineChartIcon icon { get; set; }

        public string bgImgSrc { get; set; } = "";
        public double k { get; set; } = -0.5;
        public double curvature { get; set; } = 5;
        public bool relative { get; set; } = true;
    }

    public class FlylineChartPoint
    {
        public DvPoint coordinate { get; set; } 
        public string name { get; set; } = "";
        public FlylineChartText text { get; set; }
        public FlylineChartHalo halo { get; set; }
        public FlylineChartIcon icon { get; set; }
        public FlylineChartLine line { get; set; }
        internal string key { get; set; }
    }

    public class FlylineChartFlyLine
    {
        public string source { get; set; } = "";
        public string target { get; set; } = "";
        public FlylineChartLine line { get; set; }
    }

    public class FlylineChartHalo
    {
        public bool show { get; set; } = true;
        public double[] duration { get; set; } = { 20, 30 };
        public string color { get; set; } = "#fb7293";
        public double radius { get; set; } = 120;
        internal double time { get; set; } = 0;
    }

    public class FlylineChartText
    {
        public bool show { get; set; } = false;
        public int[] offset { get; set; } = { 0, 15 };
        public string color { get; set; } = "#ffdb5c";
        public int fontSize { get; set; } = 12;
        internal double x { get; set; }
        internal double y { get; set; }
    }
    public class FlylineChartIcon
    {
        public bool show { get; set; } = false;
        public int offset { get; set; } = 0;
        public int width { get; set; } = 15;
        public int height { get; set; } = 15;
        public string src { get; set; } = "";

        internal double x { get; set; }
        internal double y { get; set; }
    }

    public class FlylineChartLine
    {
        public int width { get; set; } = 1;
        public string color { get; set; } = "#ffde93";

        public string orbitColor { get; set; } = "rgba(103, 224, 227, 0.2)";

        public double[] duration { get; set; } = { 20, 30 };

        public double radius { get; set; } = 100;
    }

    

}
