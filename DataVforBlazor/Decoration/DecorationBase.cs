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
    public class DecorationBase:ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        [Parameter]
        public virtual double Dur { get; set; } = 3;
        [Parameter]
        public virtual List<string> MergedColor { get; set; } = new List<string> { "#4fd2dd", "#235fa7" };
        protected string ID = Guid.NewGuid().ToString();
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

        protected string ToRgba(string colorString, double percent)
        {
            if (colorString.StartsWith("rgb"))
                return colorString;
            var color = ColorTranslator.FromHtml(colorString);
            return $"rgba({color.R},{color.G},{color.A},{percent})";
        }
    }
}
