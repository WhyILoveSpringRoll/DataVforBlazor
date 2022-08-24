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
    public partial class Dv_border_box_4:BoderBase
    {
        [Parameter]
        public bool Reverse { get; set; } = false;
        [Parameter]
        public override List<string> MergedColor { get; set; }= new List<string> { "red", "blue" };
    }
}
