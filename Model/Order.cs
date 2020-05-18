using System;
using System.Collections.Generic;
using System.Text;

namespace 记账.Model
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string CustomerId { get; set; }
        public string TotalPrice { get; set; }
       
        public string Remarks { get; set; }
        public int IsPayed { get; set; }
        public int IsShowed { get; set; }
        public string OrderType { get; set; }
    }
}
