using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_5 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#3f96a5", "#3f96a5" };
        public override double Dur { get; set; } = 1.2;
        private string line1Points = "", line2Points = "";
        private int line1Length = 0, line2Length = 0;
        [Parameter]
        public string Title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await CalcSVGData();
        }
        private async Task CalcSVGData()
        {
            var line1PointsList = new List<Point> {
                new Point(0, Convert.ToInt32(Height * 0.2)),
                new Point(Convert.ToInt32(Width * 0.18), Convert.ToInt32(Height * 0.2)),
                new Point(Convert.ToInt32(Width *0.2), Convert.ToInt32(Height * 0.4)),
                new Point(Convert.ToInt32(Width *0.25), Convert.ToInt32(Height * 0.4)),
                new Point(Convert.ToInt32(Width *0.27), Convert.ToInt32(Height * 0.6)),
                new Point(Convert.ToInt32(Width *0.72), Convert.ToInt32(Height * 0.6)),
                new Point(Convert.ToInt32(Width *0.75), Convert.ToInt32(Height * 0.4)),
                new Point(Convert.ToInt32(Width *0.8), Convert.ToInt32(Height * 0.4)),
                new Point(Convert.ToInt32(Width *0.82), Convert.ToInt32(Height * 0.2)),
                new Point(Convert.ToInt32(Width*1.0), Convert.ToInt32(Height * 0.2)),
            };
            var line2PointsList = new List<Point> {
                new Point(Convert.ToInt32(Width * 0.3), Convert.ToInt32(Height * 0.8)),
                new Point(Convert.ToInt32(Width * 0.7), Convert.ToInt32(Height * 0.8)),
            };

            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/datav.map.blazor.js").AsTask());
            var module = await moduleTask.Value;

        
            line1Length = Convert.ToInt32(Convert.ToDouble((await module.InvokeAsync<object>("line1Length", Width, Height)).ToString()));
            line2Length = Convert.ToInt32(Convert.ToDouble((await module.InvokeAsync<object>("line2Length", Width, Height)).ToString()));
            line1Points = string.Join(",", line1PointsList.Select(i => $"{i.X},{i.Y}"));
            line2Points = string.Join(",", line2PointsList.Select(i => $"{i.X},{i.Y}"));
        }
    }
}
