using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> GetValidUser(LoginRequest request)
        {
            var storeProcedure = "UserValidate";
            try
            {
                var connection = GetConnection();
                var response = await connection.QueryFirstOrDefaultAsync<T>(
                storeProcedure, new { 
                    request.Identificacion,
                    request.Contraseña
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
    }
}
