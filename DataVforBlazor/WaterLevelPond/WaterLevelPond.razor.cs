using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class WaterLevelPond
    {
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        
        [Parameter]
        public WaterLevelPondConfig Config { get; set; } = new WaterLevelPondConfig();
        protected string ID = Guid.NewGuid().ToString();
        private string gradientId = "";
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
            
            gradientId = $"water-level-pond-{ID}";
        }

    }
}
