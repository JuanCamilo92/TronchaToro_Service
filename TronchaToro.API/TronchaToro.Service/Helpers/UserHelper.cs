using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Context;
using TronchaToro.Service.Models.Requests;

namespace TronchaToro.Service.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IData _context;

        public UserHelper(IData context)
        {
            _context = context;
        }
        public async Task UpdateUserAsync(RegisterUserRequest request)
        {
            try
            {
                //hacer los procedimientos para crear y actualizar el usuario
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateUserAsync(RegisterUserRequest request)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
