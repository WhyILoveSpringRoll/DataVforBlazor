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
    public partial class Dv_Decoration_10 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#00c2ff", "rgba(0, 194, 255, 0.3)" };
        [Parameter]
        public override double Dur { get; set; } = 6;


        private string id = Guid.NewGuid().ToString("N");
        private string animationId1 = "";
        private string animationId2 = "";
        private string animationId3 = "";
        private string animationId4 = "";
        private string animationId5 = "";
        private string animationId6 = "";
        private string animationId7 = "";

        protected override Task OnInitializedAsync()
        {
            animationId1 = $"d10ani1{id}";
            animationId2 = $"d10ani2{id}";
            animationId3 = $"d10ani3{id}";
            animationId4 = $"d10ani4{id}";
            animationId5 = $"d10ani5{id}";
            animationId6 = $"d10ani6{id}";
            animationId7 = $"d10ani7{id}";
            return base.OnInitializedAsync();
        }


    }
}
