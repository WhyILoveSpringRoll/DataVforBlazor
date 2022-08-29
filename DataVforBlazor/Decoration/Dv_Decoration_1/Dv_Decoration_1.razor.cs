using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_1:DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#fff", "#0de7c2" };
        [Parameter]
        public int RowNum { get; set; } = 8;
        [Parameter]
        public int RowPoints { get; set; } = 40;
        private List<int> svgScale = new List<int> { 1, 1 };
        private double pointSideLength = 2.5;
        private double halfPointSideLength = 1.25;
        private List<Point> points = new List<Point>();
        private Point[] rects = new Point[] { };
        private Random rd = new Random(Guid.NewGuid().GetHashCode());


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if(RowNum==0)
            {
                RowNum = (Height / 10)-1;
                if(RowNum<=0)
                    RowNum = 0;
            }
            if (RowPoints == 0)
            {
                RowPoints=Width / 10;
                if(RowPoints<=0)
                    RowPoints=0;
            }
            CalcSVGData();
        }
        private void CalcSVGData()
        {
            CalcPointsPosition();
            CalcRectsPosition();
        }
        private void CalcPointsPosition()
        {
            points.Clear();
            var horizontalGap = Width / (RowPoints + 1);
            var verticalGap = Height / (RowNum + 1);
            for(int i=1;i<=RowNum;i++)
            {
                for(int j=1;j<=RowPoints;j++)
                {
                    points.Add(new Point(horizontalGap * j, verticalGap * i));
                }
            }
        }
        private void CalcRectsPosition()
        {
            Point p1 = points[RowPoints * 2 - 1];
            Point p2 = points[RowPoints * 2 - 3];
            rects = new Point[] { p1, p2 };
        }
    }
}
