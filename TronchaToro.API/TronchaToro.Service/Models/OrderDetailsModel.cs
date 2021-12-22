﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class OrderDetailsModel
    {
        public int IdOrderDetail { get; set; }
        public List<OrdersDetailAdditionModel> OrdersDetailAdditions { get; set; }
        public List<FoodModel> foods { get; set; }
    }
}
