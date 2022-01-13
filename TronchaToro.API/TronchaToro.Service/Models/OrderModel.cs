using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class OrderModel
    {
        public int? IdOrder { get; set; }
        public string NOrder => IdOrder == null ? "0" : $"00{IdOrder}";
        public double Total { get; set; }
        public List<OrderDetailsModel> orderDetails { get; set; }
        public int IdStatePay { get; set; }
        public int IdStateTran { get; set; }
        public string StatePay { get; set; }
        public string StateTran { get; set; }
    }
}
