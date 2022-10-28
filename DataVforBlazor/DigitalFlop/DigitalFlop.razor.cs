using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class DigitalFlop
    {

        
        [Parameter]
        public List<double> Number { get; set; } = new List<double>();
        [Parameter]
        public string Content { get; set; } = "";
        [Parameter]
        public DigitalFlopConfig Config { get; set; } = new DigitalFlopConfig();

        private string ID = Guid.NewGuid().ToString("N");
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DigitalFlop.js").AsTask());
            var module = await moduleTask.Value;
            await module.InvokeAsync<object>("Test", ID, Config);
        }

        private void InitRender()
        {
            
        }
    }
}
