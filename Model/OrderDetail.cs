using System;
using System.Collections.Generic;
using System.Text;

namespace 记账.Model
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string GoodId { get; set; }
        public int GoodCount { get; set; }
        public int GoodPrice { get; set; }
        public string Remarks { get; set; }
        public int IsEnabled { get; set; }
    }
}
