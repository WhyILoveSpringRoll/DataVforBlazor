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
    public partial class Dv_border_box_1:BoderBase
    {
        private List<string> border = new List<string> { "left-top", "right-top", "left-bottom", "right-bottom" };

        public override List<string> MergedColor { get; set; } = new List<string> { "#4fd2dd", "#235fa7" };
    }
}
