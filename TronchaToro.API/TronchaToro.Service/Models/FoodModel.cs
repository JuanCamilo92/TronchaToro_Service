using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageId { get; set; }
        public string Observations { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(ImageId)
            ? $"http://172.27.144.1:100/ImgTronchaToro/foods/no-image.png"
            : $"http://172.27.144.1:100/ImgTronchaToro/foods/{ImageId}";
    }
}
