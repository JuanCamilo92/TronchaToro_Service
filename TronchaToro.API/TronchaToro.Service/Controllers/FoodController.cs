using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TronchaToro.Service.Context;
using TronchaToro.Service.Helpers;
using TronchaToro.Service.Models;
using TronchaToro.Service.Models.Responses;

namespace TronchaToro.Service.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IData _context;
        private readonly ITokenHelper _tokenHelper;

        public FoodController(IData context, ITokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpGet]
        [Route("GetFoods")]
        public async Task<IActionResult> GetFoods()
        {
            try
            {
                Response response = await _context.GetFoods<FoodModel>();
                List<FoodModel> foodModel = (List<FoodModel>)response.Result;

                return Ok(foodModel);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
