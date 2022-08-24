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
    public partial class Dv_border_box_10:BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#1d48c4", "#d3e1f8" };

        private List<string> border = new List<string> { "left-top", "right-top", "left-bottom", "right-bottom" };
    }
}
