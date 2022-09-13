using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class ConicalColumnChart
    {
        [Parameter]
        public ConicalColumnChartConfig Config { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        private readonly string ID = Guid.NewGuid().ToString("N");
        private List<ConicalColumn> conicalColumns { get; set; }=new List<ConicalColumn>();
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

            IniData();
            CalcSVGPath();
        }

        private void IniData() 
        {
            Config.data = Config.data.OrderByDescending(i => i.value).ToList();
            var max = Config.data.Count() > 0 ? Config.data.First().value : 10;
            Config.data.ForEach(i => i.percent = i.value / 1.0 / max);
        }
        
        private void CalcSVGPath()
        {
            var itemNum = Config.data.Count() ;
            var gap = Width / (itemNum + 1.0);
            var useAbleHeight = Height - Config.imgSideLength - Config.fontSize - 5;
            var svgBottom = Height - Config.fontSize - 5;
            conicalColumns.Clear();
            foreach(var item in Config.data)
            {
                int i = Config.data.IndexOf(item);
                var middleXPos = gap * (i + 1);
                var leftXPos = gap * i;
                var rightXpos = gap * (i + 2);
                var middleYPos = svgBottom - useAbleHeight * item.percent;
                var controlYPos = useAbleHeight * item.percent * 0.6 + middleYPos;
                var d = $"M{leftXPos}, {svgBottom} Q{middleXPos}, {controlYPos} {middleXPos},{middleYPos} M{middleXPos},{middleYPos} Q{middleXPos}, {controlYPos} {rightXpos},{svgBottom} L{leftXPos}, {svgBottom} Z";
                var textY = (svgBottom + middleYPos) / 2 + Config.fontSize / 2;
                conicalColumns.Add(new ConicalColumn(item)
                {
                    d = d,
                    x=middleXPos,
                    y=middleYPos,
                    textY=textY
                });
            }
        }
        internal class ConicalColumn: ConicalData
        {
            internal string d { get; set; }
            internal double x { get; set; }
            internal double y { get; set; }
            internal double textY { get; set; } 
            internal ConicalColumn(ConicalData data)
            {
                base.value = data.value;
                base.name = data.name;
                base.percent = data.percent;
                base.img = data.img;
            }

        }
    }
}
