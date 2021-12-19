using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TronchaToro.Service.Models.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Por favor digitar el campo {0}")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor digitar el campo {0}")]
        public string Contraseña { get; set; }
    }
}
