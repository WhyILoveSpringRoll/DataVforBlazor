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
    public partial class Dv_Decoration_9 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "rgba(3, 166, 224, 0.8)", "rgba(3, 166, 224, 0.5)" };
        public override double Dur { get; set; } = 3;
        private List<double> svgScale = new List<double> { 1, 1 };
        private string id = Guid.NewGuid().ToString();
        private string polygonId = "";
        private Random rd = new Random(Guid.NewGuid().GetHashCode());
        protected override Task OnInitializedAsync()
        {
            polygonId = $"decoration-9-polygon-{id}";
            return base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            CalcScale();
        }
        private void CalcScale()
        {
            svgScale = new List<double> { Width / 100.0, Height / 100.0 };
        }
    }
}
