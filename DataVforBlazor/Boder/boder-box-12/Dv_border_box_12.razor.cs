using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataVforBlazor
{
    public partial class Dv_border_box_12:BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#2e6099", "#7ce7fd" };

        private string id = Guid.NewGuid().ToString("N");
        private string filterId = "";

        protected override Task OnInitializedAsync()
        {
            filterId = $"border-box-12-filterId-{id}";
            return base.OnInitializedAsync();
        }
    }
}
