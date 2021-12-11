using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Models;

namespace TronchaToro.Service.Helpers
{
    public interface ITokenHelper
    {
        object CrearToken(UserModel request);
        string cifrarMD5(string password);
    }
}
