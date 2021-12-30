using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class OrdersDetailAdditionModel
    {
        public int IdOrderDetailAddition { get; set; }
        public int IdorderDetail { get; set; }
        public int IdAddition { get; set; }
        public int Quantity { get; set; }
        public List<AdditionModel> additions { get; set; }
    }
}
