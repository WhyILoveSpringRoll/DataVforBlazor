﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataVforBlazor
{
    public partial class Dv_border_box_2:BoderBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#fff", "rgba(255, 255, 255, 0.6)" };
    }
}
