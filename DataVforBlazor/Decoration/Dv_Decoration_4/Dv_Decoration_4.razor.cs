using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class Dv_Decoration_4 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "rgba(255, 255, 255, 0.3)", "rgba(255, 255, 255, 0.3)" };
        [Parameter]
        public bool Reverse { get; set; } = false;
        [Parameter]
        public override double Dur { get; set; } = 3;
    }
}
