using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataVforBlazor
{
    public partial class FlylineChart : ComponentBase
    {

        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        [Parameter]
        public FlylineChartConfig Config { get; set; }

        private List<FlylineWithPath> flylines = new List<FlylineWithPath>();
        private List<int> flylineLengths = new List<int>();
        private Random rd = new Random(Guid.NewGuid().GetHashCode());
        private readonly string ID = Guid.NewGuid().ToString("N");
        private double unique;
        private string flylineGradientId { get; set; }
        private string haloGradientId { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            if (Width <= 0)
            {
                Width = int.Parse((await module.InvokeAsync<object>("GetWidth", ID)).ToString());
            }
            if (Height <= 0)
            {
                Height = int.Parse((await module.InvokeAsync<object>("GetHeight", ID)).ToString());
            }
            height = Height <= 0 ? "" : Height + "px";
            width = Width <= 0 ? "" : Width + "px";
            
            flylineGradientId = $"flylineGradient-{ID}";
            haloGradientId = $"haloGradientId-{ID}";
            unique = rd.NextDouble();
            CalcflylinePoints();

            CalcLinePaths();

            await CalcLineLengths();
        }
 

        private void CalcflylinePoints()
        {
            foreach (var item in Config.flylinePoints)
            {
                if (item.text == null)
                {
                    item.text = new FlylineChartText(Config.text);
                }
                if (item.line == null)
                {
                    item.line = new FlylineChartLine(Config.line);
                }
                if (item.icon == null)
                {
                    item.icon = new FlylineChartIcon(Config.icon);
                }
                if (item.halo == null)
                {
                    item.halo =new FlylineChartHalo(Config.halo);
                }
                if (Config.relative)
                {
                    item.coordinate = new DvPoint(item.coordinate.X * Width, item.coordinate.Y * Height);
                }
                item.halo.time = rd.NextDouble() * ((item.halo.duration[1] - item.halo.duration[0]) / 10) + (item.halo.duration[0] / 10);

                item.icon.x = (item.coordinate.X - item.icon.width / 2);
                item.icon.y = (item.coordinate.Y - item.icon.height / 2);

                item.text.x = (item.coordinate.X + item.text.offset[0]);
                item.text.y = (item.coordinate.Y + item.text.offset[1]);
                item.key = item.GetHashCode().ToString();
            }
        }
        private void CalcLinePaths()
        {
            flylines.Clear();
            foreach (var item in Config.lines)
            {
                var sourcePoint = Config.flylinePoints.FirstOrDefault(i => i.name == item.source)?.coordinate;
                var targetPoint = Config.flylinePoints.FirstOrDefault(i => i.name == item.target)?.coordinate;
                var path = GetPath(sourcePoint, targetPoint);
                var d = $"M{path[0].X},{path[0].Y} Q{path[1].X},{path[1].Y} {path[2].X},{path[2].Y}";
                var key = $"path{path.GetHashCode()}";
                flylines.Add(new FlylineWithPath
                {
                    path = path,
                    d = d,
                    key = key,
                    time = rd.NextDouble() * ((20) / 10) + (20 / 10),
                });
            }
        }

        private List<DvPoint> GetPath(DvPoint start, DvPoint end)
        {
            var controlPoint = GetControlPoint(start, end);

            return new List<DvPoint> { start, controlPoint, end };
        }
        private DvPoint GetControlPoint(DvPoint start, DvPoint end)
        {
            var midPoint = new DvPoint((start.X + end.X) / 2.0, (start.Y + end.Y) / 2.0);
            var distance = GetPointDistance(start, end);

            var targetLength = distance / Config.curvature;
            var disDived = targetLength / 2.0;
            var temPoint = new DvPoint(midPoint.X, midPoint.Y);

            do
            {
                temPoint.X += disDived;
                temPoint.Y = GetKLinePointByx(Config.k, midPoint, temPoint.X).Y;
            }
            while (GetPointDistance(midPoint, temPoint) < targetLength);

            return temPoint;
        }
        private DvPoint GetKLinePointByx(double k, DvPoint point, double dx)
        {
            var y = point.Y - k * point.X + k * dx;
            return new DvPoint(dx, y);
        }

        private double GetPointDistance(DvPoint pointOne, DvPoint pointTwo)
        {
            var minusX = Math.Abs(pointOne.X - pointTwo.X);

            var minusY = Math.Abs(pointOne.Y - pointTwo.Y);

            return Math.Sqrt(minusX * minusX + minusY * minusY);
        }

        private async Task CalcLineLengths()
        {
            await Task.Yield();
            flylineLengths.Clear();
            foreach (var item in flylines)
            {
                flylineLengths.Add(9999);
            }
        }

        internal class FlylineWithPath: FlylineChartLine
        {
            internal string d { get; set; }
            internal string key { get; set; }
            internal double time { get; set; }
            internal List<DvPoint> path { get; set; }
        }
    }
}
