using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Decoration_1:DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#fff", "#0de7c2" };

        private int rowNum = 4;
        private int rowPoints = 20;
        private List<int> svgScale = new List<int> { 1, 1 };
        private double pointSideLength = 2.5;
        private double halfPointSideLength = 1.25;
        private List<Point> points = new List<Point>();
        private Point[] rects = new Point[] { };
        private Random rd = new Random(Guid.NewGuid().GetHashCode());


        protected override Task OnInitializedAsync()
        {
            CalcSVGData();
            return base.OnInitializedAsync();
        }
        private void CalcSVGData()
        {
            CalcPointsPosition();
            CalcRectsPosition();
        }
        private void CalcPointsPosition()
        {
            points.Clear();
            var horizontalGap = Width / (rowPoints + 1);
            var verticalGap = Height / (rowNum + 1);
            for(int i=1;i<=rowNum;i++)
            {
                for(int j=1;j<=rowPoints;j++)
                {
                    points.Add(new Point(horizontalGap * j, verticalGap * i));
                }
            }
        }
        private void CalcRectsPosition()
        {
            Point p1 = points[rowPoints * 2 - 1];
            Point p2 = points[rowPoints * 2 - 3];
            rects = new Point[] { p1, p2 };
        }
    }
}
