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
    public partial class Dv_border_box_8 : BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#235fa7", "#4fd2dd" };

        [Parameter]
        public bool Reverse { get; set; } = false;
        [Parameter]
        public double Dur { get; set; } = 3;

        //private string id = Guid.NewGuid().ToString("N");
        private string id ="3b68216d67de493883ef44a545bbcb42";
        private string path = "border-box-8-path-3b68216d67de493883ef44a545bbcb42";
        private string gradient = "border-box-8-gradient-3b68216d67de493883ef44a545bbcb42";
        private string mask = "border-box-8-mask-3b68216d67de493883ef44a545bbcb42";

        protected override Task OnInitializedAsync()
        {
            //path = $"border-box-8-path-{id}";
            //gradient = $"border-box-8-gradient-{id}";
            //mask = $"border-box-8-mask-{id}";
            return base.OnInitializedAsync();
        }
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

        private string PathD()
        {
            if (Reverse)
                return $"M 2.5, 2.5 L 2.5, { Height - 2.5} L { Width - 2.5}, { Height - 2.5} L { Width - 2.5}, 2.5 L 2.5, 2.5";
            return $"M2.5, 2.5 L{ Width - 2.5}, 2.5 L{ Width - 2.5}, { Height - 2.5} L2.5, { Height - 2.5} L2.5, 2.5";
        }

        private int Length()
        {
            return (Width + Height - 5) * 2;
        }
    }
}
