using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class DecorationBase:ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        [Parameter]
        public virtual double Dur { get; set; } = 3;
        [Parameter]
        public virtual List<string> MergedColor { get; set; } = new List<string> { "#4fd2dd", "#235fa7" };
        protected string ID = Guid.NewGuid().ToString();
    }
}
