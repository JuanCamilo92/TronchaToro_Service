using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models.Requests
{
    public class OrderDetailRequest
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string descriptionFood { get; set; }
        public double priceFood { get; set; }
        public string ObservationsFood { get; set; }
        public int IdFood { get; set; }
        public int Quantity { get; set; }
        public List<OrderDetailsAdditionsRequest> OrderDetailAdditions { get; set; }
    }
}
