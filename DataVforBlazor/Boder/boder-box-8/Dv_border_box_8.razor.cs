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
    public partial class Dv_border_box_8 : BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#235fa7", "#4fd2dd" };

        [Parameter]
        public bool Reverse { get; set; } = false;
        [Parameter]
        public double Dur { get; set; } = 3;

        private string id = Guid.NewGuid().ToString("N");
        private string path = "";
        private string gradient = "";
        private string mask = "";

        protected override Task OnInitializedAsync()
        {
            path = $"border-box-8-path-{id}";
            gradient = $"border-box-8-gradient-{id}";
            mask = $"border-box-8-mask-{id}";
            return base.OnInitializedAsync();
        }
      
        private string PathD()
        {
            if (Reverse)
                return $"M 2.5, 2.5 L 2.5, { Height - 2.5} L { Width - 2.5}, { Height - 2.5} L { Width - 2.5}, 2.5 L 2.5, 2.5";
            return $"M2.5, 2.5 L{ Width - 2.5}, 2.5 L{ Width - 2.5}, { Height - 2.5} L2.5, { Height - 2.5} L2.5, 2.5";
        }

        private int Length()
        {
            return (Width + Height - 5) * 2;
        }
    }
}
