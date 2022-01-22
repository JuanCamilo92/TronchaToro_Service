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
        Task<Response> GetOrders<T>(string email, int State);
        Task<Response> GetOrdersDetail<T>(string email, int State);
        Task<Response> GetOrdersDetailAdditions<T>(string email, int State);
        Task<Response> GetAdditions<T>();
        Task<Response> GetUsers<T>();
        Task<Response> AddOrder<T>(OrderRequest orderRequest);
        Task<Response> AddOrderDetail<T>(OrderDetailRequest orderDetailRequest);
        Task<Response> AddOrderDetailAdditions<T>(List<OrderDetailsAdditionsRequest> orderDetailsAdditionsRequests);
        Task<Response> UpdateOrderDetail<T>(OrderDetailRequest orderDetailRequest);
        Task<Response> UpdateOrderDetailAdditions<T>(List<UOrderDetailsAdditionsRequest> orderDetailsAdditionsRequests);
        Task<Response> DeleteOrderDetail(int id);
    }
}
