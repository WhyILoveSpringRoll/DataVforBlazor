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
    public partial class Dv_Decoration_8 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#3f96a5", "#3f96a5" };

        [Parameter]
        public bool Reverse { get; set; } = false;

        private int XPos(int pos)
        {
            if (!Reverse) 
                return pos;
            return Width - pos;
        }
    }
}
