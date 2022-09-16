using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public partial class ScrollRankingBoard
    {

        private ScrollRankingBoardConfig _config;
        [Parameter]
        public ScrollRankingBoardConfig Config
        {
            get { 
                return _config;
            }
            set { 
                if(value!=_config)
                {
                    _config = value;
                    if(ini)
                    {
                        CalcData();
                    }
                }
            }
        }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        [Parameter]
        public bool StartScroll { get; set; } = true;
        private readonly string ID = Guid.NewGuid().ToString("N");
        private List<ScrollRankingBoardData> rows = new List<ScrollRankingBoardData>();
        private int animationIndex = 0;
        private bool ini = false;
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            Lazy<Task<IJSObjectReference>> moduleTask;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/DataVforBlazor/DataVforBlazorInterop.js").AsTask());
            var module = await moduleTask.Value;
            if (Width <= 0)
            {
                Width = int.Parse((await module.InvokeAsync<object>("GetWidth", ID)).ToString());
            }
            if (Height <= 0)
            {
                Height = int.Parse((await module.InvokeAsync<object>("GetHeight", ID)).ToString());
            }
            height = Height <= 0 ? "" : Height + "px";
            width = Width <= 0 ? "" : Width + "px";
            if (!ini)
            {
                CalcData();
                ini = true;
                Animation();
            }
        }

        private async void CalcData()
        {
            CalcRowsData();
            CalcHeights();
        }
        private void CalcRowsData()
        {
            if (Config.sort)
                Config.data = Config.data.OrderByDescending(i => i.value).ToList();
            var value = Config.data.Select(i => i.value).ToList();
            var min = value.Min();
            var minAbs = Math.Abs(min);
            var max = value.Max();
            var maxAbs = Math.Abs(max);
            var total = max + minAbs;
            for (int i = 0; i < Config.data.Count(); i++)
            {
                Config.data[i].percent = (Config.data[i].value + minAbs) / total * 100.0;
                Config.data[i].ranking = i + 1;
            }
            var rowLength = Config.data.Count;
            for (int i = 0; i < Config.data.Count(); i++)
            {
                Config.data[i].scroll = i;
            }
            this.rows = Config.data.Take(Config.data.Count).ToList();
        }
        private void CalcHeights()
        {
            var avgHeight = Height / Config.rowNum;

            Config.data.ForEach(i => i.height = avgHeight);
            rows.ForEach(i => i.height = avgHeight);
        }
        private async Task Animation()
        {
            while (true)
            {
                await Task.Delay(Config.waitTime);
                TakeData();
                StateHasChanged();
            }
        }
        private async Task TakeData()
        {
            if (!StartScroll)
                return;

            if (Config.rowNum >= Config.data.Count)
            {
                rows = Config.data;
                return;
            }

            var animationNum = Config.singleScroll ? 1 : Config.rowNum;
            if (animationNum > rows.Count)
                animationNum = rows.Count;
            for (int i = 0; i < animationNum; i++)
            {
                rows[i].remove = true;
                rows[i].height = 0;
            }
            await Task.Delay(400);
            for (int i = 0; i < animationNum; i++)
            {

                var first = rows.FirstOrDefault();
                first.remove = false;
                rows.Remove(first);
            }
            StateHasChanged();
            animationIndex++;

            if (Config.singleScroll)
            {
                //补一个进去
                var index = Config.data.IndexOf(rows.Last());
                if (index == Config.data.Count - 1)
                {
                    rows.Add(Config.data[0]);
                }
                else
                {
                    rows.Add(Config.data[index + 1]);
                }
            }
            else//按页翻，到最后一页重写补
            {
                int aaa = Config.data.Count / Config.rowNum;
                if (animationIndex >= (Config.data.Count / Config.rowNum + 1))
                {
                    animationIndex = 0;
                    this.rows = Config.data.Take(Config.data.Count).ToList();
                }
            }
            CalcHeights();
            StateHasChanged();
        }

        private void MouseOver()
        {
            if (Config.hoverPause)
            {
                StartScroll = false;
            }
        }
        private void MouseOut()
        {
            StartScroll = true;
        }
    }
}
