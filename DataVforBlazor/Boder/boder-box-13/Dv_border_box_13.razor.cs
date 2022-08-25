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
    public partial class Dv_border_box_13:BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#6586ec", "#2cf7fe" };

       
    }
}
