using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models.Requests
{
    public class UOrderDetailsAdditionsRequest
    {
        public int IdAddition { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
        public int IdDetailOrder { get; set; }
    }
}
