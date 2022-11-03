using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class ActiveRingChart
    {

        public int MaxRaidus { get; set; } = 0;
        [Parameter]
        public ActiveRingChartConfig Config { get; set; }
        private DigitalFlopConfig digitalFlopConfig { get; set; } = new DigitalFlopConfig();
        protected string ID = Guid.NewGuid().ToString();
        private Timer _timer;
        private List<double> _tempnumber = new List<double>();
        private int REFRESH_INTERVAL = 0;
        private TimeSpan _countDown = TimeSpan.Zero;
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        protected override async Task OnInitializedAsync()

        {
            digitalFlopConfig.content = "hello";
            digitalFlopConfig.fill = Config.fill;
            digitalFlopConfig.fontSize = Config.fontSize;
            digitalFlopConfig.animationCurve = Config.animationCurve;
            digitalFlopConfig.animationFrame = Config.animationFrame;
            digitalFlopConfig.toFixed = Config.toFixed;

            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/ActiveRingChart.js").AsTask());
            var module = await moduleTask.Value;
            MaxRaidus = int.Parse((await module.InvokeAsync<object>("GetMaxRadius", ID)).ToString());
            //await module.InvokeVoidAsync("SetRingOption", ID, Config);
            await Ini();
            base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync()
        {

        }

        private void SetDigitalFlopConfig()
        {
            List<double> value = Config.data.Select(i => i.value).ToList();
            double displayValue;
            if (Config.showOriginValue)
            {
                displayValue = value[Config.activeIndex];
            }
            else
            {
                double sum = value.Sum();
                double percent = (value[Config.activeIndex] / sum) * 100;
                displayValue = percent;
            }
            digitalFlopConfig.content = Config.showOriginValue ? "{0}" + Config.digitalFlopUnit : "{0}" + Config.digitalFlopUnit == "" ? "%" : Config.digitalFlopUnit;
            digitalFlopConfig.number = new List<double> { displayValue };
        }


        private async Task Ini()
        {
           await SetRingOption();
           await RingAnimation();
        }


        private async Task SetRingOption()
        {
            var option = GetRingOption();
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/ActiveRingChart.js").AsTask());
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("SetRingOption", ID, option);
        }
        private object GetRingOption()
        {
            var radius = GetRealRadius(false);
            var result = new
            {
                series = new[] {
                new {
                    type = "pie",
                    data = Config.data,
                    radius = radius,
                    outsideLabel = new { show = true },
                } },
                //color = Config.color
            };
            var aaa = result.GetType();
            return result;
        }

        private List<double> GetRealRadius(bool active)
        {
            var halfLineWidth = Config.lineWidth / 2;
            var realRadius = active ? Config.activeRadius : Config.radius;
            double radiusNum = 0;
            if (realRadius.Contains("%"))
            {
                realRadius = realRadius.Replace("%", "");
                var radius = double.Parse(realRadius) / 100;
                radiusNum = radius * MaxRaidus;
            }
            else
            {
                radiusNum = double.Parse(realRadius);
            }
            var insideRadius = radiusNum - halfLineWidth;
            var outSideRadius = radiusNum + halfLineWidth;
            return new List<double> { insideRadius, outSideRadius };
        }
        private async Task RingAnimation()
        {
            Reset();
        }
        private async Task SetTimer()
        {
            _timer = new Timer(StartCountDownForTimeSpan);
            _timer.Change(0, Config.activeTimeGap);
        }
        public async Task Reset()
        {
            if (_timer != null)
            {
                _timer?.Dispose();
            }
            await SetTimer();
        }
        private void StartCountDownForTimeSpan(object o)
        {
            var radius = GetRealRadius(false);
            var active = GetRealRadius(true);
            var option = GetRingOption();
      
        }
    }
}
