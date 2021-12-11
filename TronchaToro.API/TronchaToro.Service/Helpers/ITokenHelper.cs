using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Models.Requests;

namespace TronchaToro.Service.Helpers
{
    public interface ITokenHelper
    {
        string CrearToken(LoginRequest request);
    }
}
