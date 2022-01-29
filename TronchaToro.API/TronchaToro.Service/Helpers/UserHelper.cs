using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                await SaveImage(request.Image, "User", request.NombreImagen, request.ExtencionImagen);
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
                await SaveImage(request.Image,"User",request.NombreImagen, request.ExtencionImagen);
                await _context.RegisterUser(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveImage(byte[] file, string container, string nombre, string extencion)
        {
            try
            {
                //MemoryStream stream = new MemoryStream(file);
                //Bitmap imagen = new Bitmap(stream);
                if(!File.Exists($"/u107080449/TT/ImgTronchaToro/{container}/{nombre}{extencion}"))
                    await File.WriteAllBytesAsync($"/u107080449/TT/ImgTronchaToro/{container}/{nombre}{extencion}", file);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
