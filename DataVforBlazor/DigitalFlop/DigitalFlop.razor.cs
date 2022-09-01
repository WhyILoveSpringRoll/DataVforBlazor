using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class DigitalFlop
    {

        [Parameter]
        public List<double> Number { get; set; } = new List<double>();
        [Parameter]
        public string Content { get; set; } = "";
    }
}
