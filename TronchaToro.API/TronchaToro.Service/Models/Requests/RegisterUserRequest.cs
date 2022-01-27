using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models.Requests
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Rol_id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string imageId { get; set; }
        public string LoginType { get; set; }
        public byte[] Image { get; set; }

    }
}
