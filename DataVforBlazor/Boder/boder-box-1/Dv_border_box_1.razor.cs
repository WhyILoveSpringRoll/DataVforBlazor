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
    public partial class Dv_border_box_1
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        private string height;
        [Parameter]
        public int Width { get; set; } = 0;
        private string width;
        [Parameter]
        public string BackgroundColor { get; set; } = ColorTranslator.ToHtml(Color.Transparent);
        [Parameter]
        public List<string> MergedColor { get; set; } = new List<string> { "#4fd2dd", "#235fa7" };

        private List<string> border = new List<string> { "left-top", "right-top", "left-bottom", "right-bottom" };
        private string ID ;
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            height = Height <= 0 ? "" : Height + "px";
            width = Width <= 0 ? "" : Width + "px";

            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            Width = int.Parse((await module.InvokeAsync<object>("GetWidth", ID)).ToString());
            Console.WriteLine(Width);
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
             "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            module = await moduleTask.Value;
            Height = int.Parse((await module.InvokeAsync<object>("GetHeight", ID)).ToString());
            Console.WriteLine(Height);
    
        }
        protected override Task OnInitializedAsync()
        {
            ID = Guid.NewGuid().ToString();
            return base.OnInitializedAsync();
        }
        public async Task Click()
        {
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            Width = int.Parse((await module.InvokeAsync<object>("GetWidth", ID)).ToString());
            Console.WriteLine(Width);
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
             "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            module = await moduleTask.Value;
            Height = int.Parse((await module.InvokeAsync<object>("GetHeight", ID)).ToString());
            Console.WriteLine(Height);
        }
    }
}
