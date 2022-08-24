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
    public partial class Dv_border_box_9:BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#11eefd", "#0078d2" };
        [Parameter]
        public double Dur { get; set; } = 5;
        private string id = Guid.NewGuid().ToString("N");
        private string gradientID = "";
        private string maskID = "";
        protected override Task OnInitializedAsync()
        {
            gradientID = $"border-box-9-gradient-{id}";
            maskID = $"border-box-9-mask-{id}";
            return base.OnInitializedAsync();
        }
    }
}
