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
        public List<OrderDetailsModel> orderDetails { get; set; }
    }
}
