namespace DataVforBlazorTest
{
    using System.ComponentModel;
    public class Data
    {
        [DisplayName("参数")]
        public string Param { get; set; }

        [DisplayName("说明")]
        public string Info { get; set; }

        [DisplayName("类型")]
        public string Type { get; set; }

        [DisplayName("默认值")]
        public string DefaultValue { get; set; }
    }
}
