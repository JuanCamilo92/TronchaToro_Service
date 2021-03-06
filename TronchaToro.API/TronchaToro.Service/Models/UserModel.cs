using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class UserModel
    {
        public string Email { get; set; }
		public int Rol_id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
        public string imageId { get; set; }
        public string LoginType { get; set; }
        public string ImageFullPath =>
            string.IsNullOrEmpty(imageId)
            ? $"https://troncha-toro.com/TT/ImgTronchaToro/users/no-image.png"
            : LoginType != "1"
                ? $"https://troncha-toro.com/TT/ImgTronchaToro/users/{imageId}"
                : imageId;

        //public string ImageFullPath =>
        //    string.IsNullOrEmpty(SocialImageURL) && string.IsNullOrEmpty(imageId)
        //    ? $"http://192.168.1.65:100/ImgTronchaToro/users/no-image.png"
        //    : !string.IsNullOrEmpty(imageId)
        //        ? $"http://192.168.1.65:100/ImgTronchaToro/users/{imageId}"
        //            : !string.IsNullOrEmpty(SocialImageURL)
        //            ? SocialImageURL
        //                : $"http://192.168.1.65:100/ImgTronchaToro/users/no-image.png";

        public List<OrderModel> orders { get; set; }
        public int NOrders { get; set; }
    }
}
