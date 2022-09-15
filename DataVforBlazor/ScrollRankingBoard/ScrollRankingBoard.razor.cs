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

        [Parameter]
        public ScrollRankingBoardConfig Config { get; set; }
        [Parameter]
        public int Height { get; set; } = 0;
        protected string height;
        [Parameter]
        public int Width { get; set; } = 0;
        protected string width;
        private readonly string ID = Guid.NewGuid().ToString("N");
        private List<ScrollRankingBoardData> rows = new List<ScrollRankingBoardData>();
        private List<int> heights = new List<int>();
        private int animationIndex = 0;
        private string animationHandler = "";
        private int currentIndex = 0;
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
            if(!ini)
            {
                CalcData();
                ini = !ini;
            }
        }
        
        private async void CalcData()
        {
            CalcRowsData();
            CalcHeights();
            //Animation(true);
        }
        private void CalcRowsData()
        {
            if(Config.sort)
                Config.data = Config.data.OrderByDescending(i => i.value).ToList();
            var value = Config.data.Select(i => i.value).ToList();
            var min = value.Min();
            var minAbs = Math.Abs(min);
            var max = value.Max();
            var maxAbs = Math.Abs(max);
            var total = max + minAbs;
            for(int i=0;i<Config.data.Count();i++)
            {
                Config.data[i].percent = (Config.data[i].value + minAbs) / total * 100.0;
                Config.data[i].ranking = i + 1;
            }
            var rowLength = Config.data.Count;
            //if(rowLength>Config.rowNum && rowLength<2*Config.rowNum)
            //{
            //     Config.data.CopyTo(Config.data.ToArray(),0);
            //}
            for (int i = 0; i < Config.data.Count(); i++)
            {
                Config.data[i].scroll = i;
            }
            this.rows = Config.data.Take(Config.rowNum * 2).ToList(); ;
        }
        private void CalcHeights()
        {
            var  avgHeight = Height / Config.rowNum;
            // this.heights = Enumerable.Range(0, Config.data.Count).Select(i => avgHeight).ToList();
            Config.data.ForEach(i => i.height = avgHeight);
            rows.ForEach(i => i.height = avgHeight);
        }
        public async void Animation(bool start)
        {
            var rowLength = Config.data.Count;
            if (Config.rowNum >= rowLength) 
                return;
            if(start)
            {
                await Task.Delay(Config.waitTime);
            }
            var animationNum = Config.carousel == "single" ? 1 : Config.rowNum;

          
        }
        public async void TakeData()
        {
            
            if(Config.rowNum+1>=Config.data.Count)
            {
                rows = Config.data;
                return;
            }
          
            var animationNum = Config.carousel == "single" ? 1 : Config.rowNum;
            for(int i=0;i<animationNum;i++)
            {
                rows[0].height = 0;
            }
            await Task.Delay(900);
            for (int i = 0; i < animationNum; i++)
            {
                rows.RemoveAt(0);
            }
            StateHasChanged();
            animationIndex += animationNum;
            
            if(animationNum==1)
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
            else
            {
                
            }


            CalcHeights();
            StateHasChanged();
        } 
    }
}
