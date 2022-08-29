using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_3:DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#7acaec", "transparent" };
        [Parameter]
        public int RowNum { get; set; } = 2;
        [Parameter]
        public int RowPoints { get; set; } = 25;
        private List<int> svgScale = new List<int> { 1, 1 };
        private double pointSideLength = 7;
        private double halfPointSideLength = 3.5;
        private List<Point> points = new List<Point>();
        private Point[] rects = new Point[] { };
        private Random rd = new Random(Guid.NewGuid().GetHashCode());


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if(RowNum==0)
            {
                RowNum = ((Height-15) / 10);
                if(RowNum<=0)
                    RowNum = 0;
            }
            if (RowPoints == 0)
            {
                RowPoints=(Width-50) / 10;
                if(RowPoints<=0)
                    RowPoints=0;
            }
            CalcSVGData();
        }
        private void CalcSVGData()
        {
            CalcPointsPosition();
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
     
    }
}
