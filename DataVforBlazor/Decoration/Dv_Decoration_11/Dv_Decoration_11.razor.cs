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
    public partial class Dv_Decoration_11 : DecorationBase
    {
        public override List<string> MergedColor { get; set; } = new List<string> { "#1a98fc", "#2cf7fe" };

        private string ToRgba(string colorString, double percent)
        {
            if (colorString.StartsWith("rgb"))
                return colorString;

            var color = ColorTranslator.FromHtml(colorString);
            return $"rgba({color.R},{color.G},{color.A},{percent})";
        }

    }
}
