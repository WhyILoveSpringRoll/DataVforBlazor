using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class DigitalFlopConfig
    {
        public List<double> number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _oldnumber = _number.ToList();
                    _number = value;
                    OnNumberChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        internal List<double> _oldnumber = new List<double>();
        private List<double> _number { get; set; } = new List<double>();
        public string content { get; set; } = "";
        public string text { get; set; } = "";
        public int toFixed { get; set; } = 0;
        public string textAlign { get; set; } = "center";// 'center' | 'left' | 'right'
        public int rowGap { get; set; } = 0;
        public int fontSize { get; set; } = 30;
        public string fill { get; set; } = "#3de7c9";
        public string animationCurve { get; set; } = "easeOutCubic";
        public int animationFrame { get; set; } = 20;
        public delegate void NumberChanged(object sender, EventArgs e);
        public event NumberChanged OnNumberChanged;
        public DigitalFlopConfig()
        {
            
        }
        public DigitalFlopConfig(DigitalFlopConfig config)
        {
            number = config.number.ToList();
            content = config.content;
            text = config.text;
            toFixed = config.toFixed;
            textAlign = config.textAlign;
            rowGap = config.rowGap;
            fontSize = config.fontSize;
            fill = config.fill;
            animationCurve = config.animationCurve;
            animationFrame = config.animationFrame;
            _oldnumber = config._oldnumber.ToList();
        }
    }
}
