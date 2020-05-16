using System;
using System.Collections.Generic;
using System.Text;

namespace 记账.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public int IsEnabled { get; set; }
        public string CustomerType { get; set; }
    }
}
