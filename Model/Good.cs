using System;
using System.Collections.Generic;
using System.Text;

namespace 记账.Model
{
    public class Good
    {
        public string GoodName { get; set; }
        public decimal SellPrice { get; set; }
        public decimal InPrice { get; set; }
        public string Remarks { get; set; }
        public bool IsEnabled { get; set; }
        public string Unit { get; set; }
    }
}
