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
    public partial class Dv_border_box_7:BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "rgba(128,128,128,0.3)", "rgba(128,128,128,0.5)" };
    }
}
