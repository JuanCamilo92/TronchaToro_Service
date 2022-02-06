using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Context;
using TronchaToro.Service.Models;
using TronchaToro.Service.Models.Requests;
using TronchaToro.Service.Models.Responses;

namespace TronchaToro.Service.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IData _context;
        private readonly IHostingEnvironment _env;

        public UserHelper(IData context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task UpdateUserAsync(RegisterUserRequest request)
        {
            try
            {
                if (request.Image != null)
                    await SaveImage(request.Image, "User", request.imageId);
                await _context.RegisterUserUpdate(request);
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
                if (request.Image != null)
                    await SaveImage(request.Image, "users", request.imageId);
                Response response = await _context.RegisterUser(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveImage(byte[] file, string container, string nombre)
        {
            try
            {
                //MemoryStream stream = new MemoryStream(file);
                //Bitmap imagen = new Bitmap(stream);
                if (!File.Exists(_env.ContentRootPath+$"/ImgTronchaToro/{container}/{nombre}"))
                    await File.WriteAllBytesAsync(_env.ContentRootPath + $"/ImgTronchaToro/{container}/{nombre}", file);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
