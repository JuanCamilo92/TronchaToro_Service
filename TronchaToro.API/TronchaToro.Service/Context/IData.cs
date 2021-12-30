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
        Task<Response> GetUser<T>(string email);
        Task<Response> GetFoods<T>();
        Task<Response> GetOrders<T>(string email);
        Task<Response> GetOrdersDetail<T>(string email);
        Task<Response> GetOrdersDetailAdditions<T>(string email);
        Task<Response> GetAdditions<T>();
        Task<Response> GetUsers<T>();
    }
}
