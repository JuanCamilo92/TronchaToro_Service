using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class OrderModel
    {
        public int IdOrder { get; set; }
        public string NOrder => $"00{Id}";
        public List<OrderDetailsModel> orderDetails { get; set; }
    }
}
