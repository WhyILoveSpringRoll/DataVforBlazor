using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_6:DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#7acaec", "#7acaec" };
        [Parameter]
        public int RowNum { get; set; } = 1;
        [Parameter]
        public int RowPoints { get; set; } = 40;
        private List<int> svgScale = new List<int> { 1, 1 };
        private double rectWidth = 7;
        private double halfRectWidth = 3.5;
        private List<Point> points = new List<Point>();
        private List<double> randoms = new List<double>();
        private List<double> heights = new List<double>();
        private List<double> minHeights = new List<double>();

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
            for (int i = 1; i <= RowNum; i++)
            {
                for (int j = 1; j <= RowPoints; j++)
                {
                    points.Add(new Point(horizontalGap * j, verticalGap * i));
                }
            }

            heights.Clear();
            for(int i=0;i<RowNum*RowPoints;i++)
            {
                if(rd.NextDouble()>0.8)
                {
                    heights.Add(RandomExtend(0.7 * Height, Height));
                }
                else
                {
                    heights.Add(RandomExtend(0.2 * Height,0.5*Height));
                }
            }
            minHeights = heights.Select(i => i * rd.NextDouble()).ToList();
            randoms= heights.Select(i => 1.5 + rd.NextDouble()).ToList();
        }

        private int RandomExtend(double minNum, double maxNum)
        {
            return Convert.ToInt32(rd.NextDouble() * (maxNum - minNum + 1) + minNum);
        }

    }
}

