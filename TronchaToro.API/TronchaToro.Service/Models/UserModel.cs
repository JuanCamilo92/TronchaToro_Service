using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models
{
    public class UserModel
    {
        public string Email { get; set; }
		public int Rol_id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string BirthDate { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string imageId { get; set; }
		public string SocialImageURL { get; set; }
		public string ImageFullPath => string.IsNullOrEmpty(SocialImageURL)
			? string.IsNullOrEmpty(imageId)
				? $"https://vehiclesapijs.azurewebsites.net/Images/no-image.png"
				: $"https://vehiclesjcsg.blob.core.windows.net/users/{imageId}"
			: String.IsNullOrEmpty(SocialImageURL)
				? $"https://vehiclesapijs.azurewebsites.net/Images/no-image.png"
				: SocialImageURL;
	}
}
