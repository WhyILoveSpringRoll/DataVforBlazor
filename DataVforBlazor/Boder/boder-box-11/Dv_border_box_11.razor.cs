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
    public partial class Dv_border_box_11 : BoderBase
    {
        [Parameter]
        public override List<string> MergedColor { get; set; } = new List<string> { "#8aaafb", "#1f33a2" };
        [Parameter]
        public string Title { get; set; } = "Title";
        [Parameter]
        public int TitleWidth { get; set; } =250;

        private string id = Guid.NewGuid().ToString("N");
        private string filterId = "";

        protected override Task OnInitializedAsync()
        {
            filterId = $"border-box-9-filterId-{id}";
            return base.OnInitializedAsync();
        }
    }
}
