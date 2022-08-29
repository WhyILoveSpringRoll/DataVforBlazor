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
    public partial class Dv_Decoration_2 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#3faacb", "#fff" };
        [Parameter]
        public bool Reverse { get; set; } = false;
        [Parameter]
        public override double Dur { get; set; } = 6;

        private int x = 0, y = 0, w = 0, h = 0;


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            CalcSVGData();
        }
        private void CalcSVGData()
        {
            if (Reverse)
            {
                this.w = 1;
                this.h = Height;
                this.x = Width / 2;
                this.y = 0;
            }
            else
            {
                this.w = Width;
                this.h = 1;
                this.x = 0;
                this.y = Height / 2;
            }
        }

    }
}
