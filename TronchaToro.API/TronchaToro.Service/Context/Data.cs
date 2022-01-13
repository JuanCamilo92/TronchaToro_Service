﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Models;
using TronchaToro.Service.Models.Requests;
using TronchaToro.Service.Models.Responses;

namespace TronchaToro.Service.Context
{
    public class Data : IData
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        public Data(IConfiguration configs)
        {
            _configuration = configs;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
        
        public async Task<Response> GetValidUser(LoginRequest request)
        {
            var storeProcedure = "UserValidate";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryFirstOrDefaultAsync(
                storeProcedure, new { 
                    Email =request.Email,
                    Password = request.Contraseña
                },
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetUser<T>(string email)
        {
            var storeProcedure = "GetUser";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryFirstOrDefaultAsync<T>(
                storeProcedure, new
                {
                    Email = email
                },
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetUsers<T>()
        {
            var storeProcedure = "GetUsers";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure,
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetFoods<T>()
        {
            var storeProcedure = "GetFoods";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure,
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetAdditions<T>()
        {
            var storeProcedure = "GetAdditions";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure,
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response> GetOrders<T>(string email, int State)
        {
            var storeProcedure = "GetOrders";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure, new { email, State },
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetOrdersDetail<T>(string email, int State)
        {
            var storeProcedure = "GetOrdersDetail";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure, new { email, State },
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetOrdersDetailAdditions<T>(string email, int State)
        {
            var storeProcedure = "GetOrdersDetailAdditions";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryAsync<T>(
                storeProcedure, new { email, State },
                commandType: CommandType.StoredProcedure);

                CloseConnection();

                Response Respuesta = new Response()
                {
                    Result = response,
                    IsSuccess = true
                };

                if (Respuesta.Result == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay resultados"
                    };
                }

                return Respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public async Task<Response> GetOrdersInfo()
        //{
        //    var storeProcedure = "GetOrders";
        //    try
        //    {
        //        var connection = GetConnection();
        //        var response = await connection.QueryAsync<OrderModel,
        //                                    List<OrderDetailsModel>, OrderModel>(
        //        storeProcedure,
        //        (IdOrder, IdOrderDetail) =>
        //        {

        //                List<OrderDetailsModel> Detail = IdOrderDetail.Where(x => x.IdOrderDetail == IdOrder.IdOrder).ToList();
        //            IdOrder.orderDetails = new List<OrderDetailsModel>();
        //            IdOrder.orderDetails = Detail;


        //            return IdOrder;
        //        }//, new { email }
        //        ,
        //        commandType: CommandType.StoredProcedure,
        //        splitOn: "IdOrder,IdDetail");

        //        CloseConnection();

        //        Response Respuesta = new Response()
        //        {
        //            Result = response,
        //            IsSuccess = true
        //        };

        //        if (Respuesta.Result == null)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = "No hay resultados"
        //            };
        //        }

        //        return Respuesta;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
