using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Models.Requests;

namespace TronchaToro.Service.Helpers
{
    public interface IUserHelper
    {
        Task UpdateUserAsync(RegisterUserRequest request);
        Task CreateUserAsync(RegisterUserRequest request);
    }
}
