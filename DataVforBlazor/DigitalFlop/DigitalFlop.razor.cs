using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
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
        private DigitalFlopConfig _config { get; set; } = new DigitalFlopConfig();
        [Parameter]
        public DigitalFlopConfig Config
        {
            get
            {
                return _config;
            }
            set
            {
                if (value != _config)
                {
                    _config = value;
                }
            }
        }

        private Timer _timer;
        private readonly int animationTime = 750;
        private List<double> _tempnumber = new List<double>();
        private  int REFRESH_INTERVAL =0;
        private TimeSpan _countDown = TimeSpan.Zero;
        private string ID = Guid.NewGuid().ToString("N");
        [Inject]
        protected IJSRuntime? jsRuntime { get; set; }
        protected override void OnInitialized()
        {
            REFRESH_INTERVAL = animationTime / Config.animationFrame;
            Convert(Config);
            Render(Config);
            Config.OnNumberChanged += OnNumberChanged;
            base.OnInitialized();
        }
        
        private void Convert(DigitalFlopConfig config)
        {
            var tempstring = "";
            tempstring = config.content;
            for (int i=0;i<config.number.Count;i++)
            {
                tempstring = tempstring.Replace("{" + i + "}", config.number[i].ToString("f" + config.toFixed));
            }
            config.text = tempstring;
        }
        private async void Render(DigitalFlopConfig config)
        {
            Convert(config);
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DigitalFlop.js").AsTask());
            var module = await moduleTask.Value;
            await module.InvokeAsync<object>("DigitalFlop", ID, config);
        }


        private void OnNumberChanged(object sender, EventArgs e)
        {
            Reset();
        }
    

        private void StartCountDownForTimeSpan(object o)
        {
            _countDown = _countDown.Add(new TimeSpan(-REFRESH_INTERVAL));
            DigitalFlopConfig config = new DigitalFlopConfig(Config);
            config.number.Clear();
            for (int i = 0; i < _tempnumber.Count; i++)
            {
                if (_countDown.Ticks <= 0)
                {
                    config.number.Add(Config.number[i]);
                    _countDown = TimeSpan.Zero;
                    _timer.Dispose();
                }
                else
                {
                    //运动学公式s=1/2at^2
                    var a = 2 * (Config.number[i] - config._oldnumber[i]) / animationTime / animationTime;
                    var deltaS = 0.5 * a * _countDown.Ticks * _countDown.Ticks - 0.5 * a * (_countDown.Ticks + REFRESH_INTERVAL) * (_countDown.Ticks + REFRESH_INTERVAL);
                    //线性运动
                    //config.number.Add(_tempnumber[i] += ((Config.number[i] - config._oldnumber[i]) / Config.animationFrame));
                    //加速运动
                    config.number.Add(_tempnumber[i] -= deltaS);
                }
            }
            Render(config);
        }
        private async Task SetTimer()
        {
            _tempnumber = Config._oldnumber.ToList();
            _countDown = new TimeSpan(animationTime);
            _timer = new Timer(StartCountDownForTimeSpan);
            _timer.Change(0, REFRESH_INTERVAL);
        }
        public async Task Reset()
        {
            if (_timer != null)
            {
                _timer?.Dispose();
            }
            await SetTimer();
        }
    }
}
