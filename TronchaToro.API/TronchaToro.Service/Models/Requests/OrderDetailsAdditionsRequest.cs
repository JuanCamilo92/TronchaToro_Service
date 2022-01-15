using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models.Requests
{
    public class OrderDetailsAdditionsRequest
    {
        public int IdDetailOrder { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int IdAddition { get; set; }
        public int Quantity { get; set; }
    }
}
