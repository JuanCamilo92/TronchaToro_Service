using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class FoodModel
    {
        public int IdFood { get; set; }
        public string DescriptionFood { get; set; }
        public int PriceFood { get; set; }
        public string ImageId { get; set; }
        public string Observations { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(ImageId)
            ? $"https://troncha-toro.com/TT/ImgTronchaToro/foods/no-image.png"
            : $"https://troncha-toro.com/TT/ImgTronchaToro/foods/{ImageId}";
        //public string ImageFullPath => string.IsNullOrEmpty(ImageId)
        //    ? $"http://192.168.1.65:100/ImgTronchaToro/foods/no-image.png"
        //    : $"http://192.168.1.65:100/ImgTronchaToro/foods/{ImageId}";
    }
}
