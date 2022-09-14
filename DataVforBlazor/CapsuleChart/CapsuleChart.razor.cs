using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class CapsuleChart
    {
        [Parameter]
        public CapsuleChartConfig Config { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        private readonly string ID = Guid.NewGuid().ToString("N");
        private List<double> capsuleValue = new List<double>();
        private List<double> capsuleLength = new List<double>();
        private List<double> labelData = new List<double>();
        private List<double> labelDataLength = new List<double>();

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

            CalcCapsuleLengthAndLabelData();
        }

        private void CalcCapsuleLengthAndLabelData()
        {
            if(Config.data.Count==0)
                return;
            if(Config.sort)
                Config.data = Config.data.OrderByDescending(x => x.value).ToList();
            
            this.capsuleValue = Config.data.Select(i => i.value).ToList();
            var maxValue = Config.data.Max(i => i.value);
            this.capsuleLength = capsuleValue.Select(i => i / 1.0 / maxValue).ToList();
            var oneFifth = maxValue / 5;
            this.labelData=Enumerable.Range(0, 6).Select(i => Math.Ceiling(i * oneFifth)).ToList();
            this.labelDataLength = Enumerable.Range(0, labelData.Count()).Select(i=>i/1.0/maxValue).ToList();
        }


    }
}
