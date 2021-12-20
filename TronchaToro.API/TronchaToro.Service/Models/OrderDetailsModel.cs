using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class OrderDetailsModel
    {
        public int IdOrderDetail { get; set; }
        public List<OrdersDetailAdditionModel> OrdersDetailAdditions { get; set; }
    }
}
