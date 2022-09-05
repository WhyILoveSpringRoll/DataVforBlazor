using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
        public FlylineChartConfig config { get; set; }


        private Random rd = new Random(Guid.NewGuid().GetHashCode());
        private readonly string ID = Guid.NewGuid().ToString("N");
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
        }
        protected override Task OnInitializedAsync()
        {
            flylineGradientId = $"flylineGradient-{ID}";
            haloGradientId = $"haloGradientId-{ID}";
            return base.OnInitializedAsync();
        }

        private void CalcflylinePoints()
        {
            foreach (var item in config.points)
            {
                if (item.text == null)
                {
                    item.text = config.text;
                }
                if (item.line == null)
                {
                    item.line = config.line;
                }
                if (item.icon == null)
                {
                    item.icon = config.icon;
                }
                if (item.halo == null)
                {
                    item.halo = config.halo;
                }
                if (config.relative)
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
            foreach (var item in config.lines)
            {
                var sourcePoint = config.points.FirstOrDefault(i => i.name == item.source)?.coordinate;
                var targetPoint = config.points.FirstOrDefault(i => i.name == item.target)?.coordinate;
            }
        }

        private void GetPath()
        {

        }
        private void GetControlPoint(DvPoint start, DvPoint end)
        {
            var midPoint = new DvPoint((start.X + end.X) / 2.0, (start.Y + end.Y) / 2.0);
            var distance = GetPointDistance(start, end);

            var targetLength = distance / config.curvature;
            var disDived = targetLength / 2.0;
            var temPoint = new DvPoint(midPoint.X, midPoint.Y);

            do
            {
                temPoint.X += disDived;
                temPoint.Y = GetKLinePointByx(config.k, midPoint, temPoint.X).Y;
            }
            while (GetPointDistance(midPoint, temPoint) < targetLength);
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
    }
}
