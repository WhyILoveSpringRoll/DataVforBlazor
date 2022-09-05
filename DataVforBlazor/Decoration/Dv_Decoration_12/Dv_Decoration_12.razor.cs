using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_12 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#2783ce", "#2cf7fe" };
        [Parameter]
        public double ScanDur { get; set; } = 3;
        [Parameter]
        public double HaloDur { get; set; } = 2;

        private string id = Guid.NewGuid().ToString("N");
        private string gradientId = "";
        private string gId = "";
        private List<string> pathD = new List<string>();
        private List<string> pathColor = new List<string>();
        private List<double> circleR = new List<double>();
        private List<string> splitLinePoints = new List<string>();
        private List<string> arcD = new List<string>();
        private int segment = 30;
        private double sectorAngle = Math.PI / 3.0;
        private int ringNum = 3;
        private int ringWidth = 1;
        private bool showSplitLine = true;

        protected override Task OnInitializedAsync()
        {
            gId = $"decoration-12-g-{id}";
            gradientId = $"decoration-12-gradient-{id}";
            return base.OnInitializedAsync();
        }

       

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            CalcPathD();

            CalcPathColor();

            CalcCircleR();

            CalcSplitLinePoints();

            CalcArcD();
        }

        private int GetX()
        {
            return Width / 2;
        }
        private int GetY()
        {
            return Height / 2;
        }

        private void CalcPathD()
        {
            double startAngle = -Math.PI / 2.0;
            double angleGap = sectorAngle / segment;
            double r = Width / 4.0;
            DvPoint lastEndPoint = GetCircleRadianPoint(GetX(), GetY(), r, startAngle);

            pathD.Clear();
            for (int i = 0; i < segment; i++)
            {
                var endPoint = GetCircleRadianPoint(GetX(), GetY(), r, startAngle - (i + 1) * angleGap);
                string d = $"M{lastEndPoint.X},{lastEndPoint.Y} A{r}, {r} 0 0 0 {endPoint.X},{endPoint.Y}";
                lastEndPoint = endPoint;
                pathD.Add(d);
            }
        }

        private DvPoint GetCircleRadianPoint(int x, int y, double radius, double radian)
        {
            return new DvPoint(x + Math.Cos(radian) * radius, y + Math.Sin(radian) * radius);
        }
        private void CalcPathColor()
        {
            double colorGap = 100.0 / (segment - 1);
            pathColor.Clear();
            for (int i = 0; i < segment; i++)
            {
                pathColor.Add(ToRgba(MergedColor[0], ((100 - colorGap * i)/100.0)));
            }
        }
        private void CalcCircleR()
        {
            double radiusGap = (Width / 2.0 - ringWidth / 2.0) / ringNum;
            circleR.Clear();
            for (int i = 0; i < ringNum; i++)
            {
                circleR.Add(radiusGap * (i + 1));
            }
        }
        private void CalcSplitLinePoints()
        {
            double angleGap = Math.PI / 6.0;
            double r = Width / 2.0;

            splitLinePoints.Clear();
            for (int i = 0; i < 6; i++)
            {
                var startAngle = angleGap * (i + 1);
                var endAngle = startAngle + Math.PI;
                var startPoint = GetCircleRadianPoint(GetX(), GetY(), r, startAngle);
                var endPoint = GetCircleRadianPoint(GetX(), GetY(), r, endAngle);
                splitLinePoints.Add($"{startPoint.X},{startPoint.Y} {endPoint.X},{endPoint.Y}");
            }
        }
        private void CalcArcD()
        {
            double angleGap = Math.PI / 6.0;
            double r = Width / 2.0 - 1;

            arcD.Clear();
            for (int i = 0; i < 4; i++)
            {
                var startAngle = angleGap * (3 * i + 1);
                var endAngle = startAngle + angleGap;
                var startPoint = GetCircleRadianPoint(GetX(), GetY(), r, startAngle);
                var endPoint = GetCircleRadianPoint(GetX(), GetY(), r, endAngle);
                arcD.Add($"M{startPoint.X},{startPoint.Y} A{GetX()}, {GetY()} 0 0 1 {endPoint.X},{endPoint.Y}");
            }
        }
    }
}
