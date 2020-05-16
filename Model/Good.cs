using System;
using System.Collections.Generic;
using System.Text;

namespace 记账.Model
{
    public class Good
    {
        public string Id { get; set; }
        public string GoodName { get; set; }
        public string GoodType { get; set; }
        public string SellPrice { get; set; }
        public string InPrice { get; set; }
        public string Remarks { get; set; }
        public int IsEnabled { get; set; }
        public string Unit { get; set; }
        public string SinglePrice { get; set; }
    }
}
