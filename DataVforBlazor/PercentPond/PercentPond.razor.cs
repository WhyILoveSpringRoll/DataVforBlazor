using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class PercentPond
    {
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        
        [Parameter]
        public PercentPondConfig Config { get; set; } 
        protected string ID = Guid.NewGuid().ToString();
        private string gradientId1 = "";
        private string gradientId2 = "";
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

            gradientId1 = $"percent-pond-gradientId1-{ID}";
            gradientId2 = $"percent-pond-gradientId2-{ID}";
        }
        
        private int RectWidth()
        {
            if (Config == null)
                return 0;
            return Width - Config.borderWidth;
        }
        private int RectHeight()
        {
            if (Config == null)
                return 0;
            return Height - Config.borderWidth;
        }
        private string Points()
        {
            var halfHeight = Height / 2;
            if (Config == null)
                return $"0, {halfHeight} 0, {halfHeight}";
            var polylineLength = (Width - (Config.borderWidth + Config.borderGap) * 2) / 100.0 * Config.value;
            return $"{Config.borderWidth + Config.borderGap}, {halfHeight}  {Config.borderWidth + Config.borderGap + polylineLength}, {halfHeight + 0.001}";
        }
        private int PolylineWidth()
        {
            if (Config == null)
                return 0;
            return Height - (Config.borderWidth + Config.borderGap) * 2;
        }
        private List<KeyValuePair<double,string>> LinearGradient()
        {
            if(Config==null)
                return new List<KeyValuePair<double, string>>();
            var colorNum = Config.colors.Count;
            var colorOffsetGap = 100 / (colorNum - 1);
            return  Config.colors.Select((item, Index) => new KeyValuePair<double, string>(colorOffsetGap * Index, item)).ToList();
        }
        private string PolylineGradient()
        {
            if (Config == null)
                return gradientId2;
            if(Config.localGradient)
                return gradientId1;
            return gradientId2;
        }
        private string Gradient2XPos()
        {
            if (Config == null)
                return "100%";
            return $"{200-Config.value}%";
        }
        private string Details()
        {
            if (Config == null)
                return "";
            return Config.formatter.Replace("{value}", Config.value.ToString());
        }


    }
}
