using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
   
    public static class JSHelper
    {
        [Inject]
        public static IJSRuntime jsRuntime { get; set; }
        private static  Lazy<Task<IJSObjectReference>> moduleTask;
        public static async Task<int> GetWidth(string ID)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            return int.Parse((await module.InvokeAsync<object>("GetWidth", ID)).ToString());
        }
        public static async Task<int> GetHeight(string ID)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            return int.Parse((await module.InvokeAsync<object>("GetHeight", ID)).ToString());
        }
    }
}
