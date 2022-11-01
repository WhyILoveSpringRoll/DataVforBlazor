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
                    Animate();
                    _newnumber = value.number.ToList();
                    _config = value;
                }
            }
        }
        
        private Timer _timer;
        private List<double> _tempnumber = new List<double>();
        private List<double> _newnumber = new List<double>();
        private const int REFRESH_INTERVAL = 1000 / 30;
        private TimeSpan _countDown = TimeSpan.Zero;
        private string ID = Guid.NewGuid().ToString("N");
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected override void OnInitialized()
        {
            Convert();
            Render(Config);
            Config.OnNumberChanged += OnNumberChanged;

            base.OnInitialized();
        }

        private void Convert()
        {
            Config.text = Config.content.Replace("{nt}", Config.number[0].ToString());
        }
        private async void Render(DigitalFlopConfig config)
        {
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DigitalFlop.js").AsTask());
            var module = await moduleTask.Value;
            await module.InvokeAsync<object>("Test", ID, config);
        }
        
       
        void OnNumberChanged(object sender, EventArgs e)
        {
            Animate();
        }
        private async void Animate()
        {
            await Reset();
        }

        private void StartCountDownForTimeSpan(object o)
        {
            _countDown = _countDown.Add(TimeSpan.FromMilliseconds(-REFRESH_INTERVAL));
            DigitalFlopConfig config = new DigitalFlopConfig(Config);
            config.number.Clear();
            for (int i = 0; i < _tempnumber.Count; i++)
            {
                config.number.Add(_tempnumber[i] += ((Config.number[i] - _newnumber[i])/30.0));
            }
            Console.WriteLine(config.number[0]);
            Render(config);
            if (_countDown.Ticks <= 0)
            {
                _countDown = TimeSpan.Zero;
                _timer.Dispose();
            }
        }
        private async Task SetTimer()
        {
            Console.WriteLine("settimer");
            _countDown = new TimeSpan(1000);
            _timer = new Timer(StartCountDownForTimeSpan);
            _timer.Change(0, REFRESH_INTERVAL);
        }
        public async Task Reset()
        {
            //避免初始化时调用Reset
            if (_timer == null)
            {
                _timer?.Dispose();
                await SetTimer();
            }
        }
    }
}
