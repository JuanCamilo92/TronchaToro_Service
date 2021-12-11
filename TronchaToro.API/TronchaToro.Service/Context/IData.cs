using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Models.Requests;
using TronchaToro.Service.Models.Responses;

namespace TronchaToro.Service.Context
{
    public interface IData
    {
        Task<Response> GetValidUser(LoginRequest request);
        Task<Response> GetUser<T>(LoginRequest request);
    }
}
