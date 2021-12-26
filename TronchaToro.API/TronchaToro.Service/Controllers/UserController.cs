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
using TronchaToro.Service.Models.Requests;
using TronchaToro.Service.Models.Responses;

namespace TronchaToro.Service.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IData _context;
        private readonly ITokenHelper _tokenHelper;

        public UserController(IData context, ITokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("CreateToken")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Usuario o contraseña son incorrectos");

                //Encriptar password
                string contraseña = _tokenHelper.cifrarMD5(model.Contraseña);
                model.Contraseña = contraseña;

                //Validar password
                Response responseValid = await _context.GetValidUser(model);
                if (responseValid.Result == null)
                    return BadRequest("Usuario o contraseña son incorrectos");

                //Consulto el usuario con todos los datos
                Response response = await _context.GetUser<UserModel>(model.Email);
                UserModel user = (UserModel)response.Result;

                //Crear token con el usuario completo
                object token = _tokenHelper.CrearToken(user);

                return Created(string.Empty, token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserInfo/{email}")]
        public async Task<IActionResult> GetUserInfo(string email)
        {
            try
            {
                Response response = await _context.GetUser<UserModel>(email);
                UserModel foodModel = (UserModel)response.Result;

                return Ok(foodModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrdersInfo/{email}")]
        public async Task<IActionResult> GetOrdersInfo(string email)
        {
            try
            {
                Response userResponse = await _context.GetUser<UserModel>(email);
                UserModel users = (UserModel)userResponse.Result;

                Response OrdersResponse = await _context.GetOrders<OrderModel>(email);
                List<OrderModel> orders = (List<OrderModel>)OrdersResponse.Result;

                Response OrdersDetailResponse = await _context.GetOrdersDetail<OrderDetailsModel>(email);
                List<OrderDetailsModel> ordersDetail = (List<OrderDetailsModel>)OrdersDetailResponse.Result;

                Response OrdersDetailAdditionsResponse = await _context.GetOrdersDetailAdditions<OrdersDetailAdditionModel>(email);
                List<OrdersDetailAdditionModel> ordersDetailAddition = (List<OrdersDetailAdditionModel>)OrdersDetailAdditionsResponse.Result;

                Response FoodResponse = await _context.GetFoods<FoodModel>();
                List<FoodModel> foods = (List<FoodModel>)FoodResponse.Result;

                Response AdditionResponse = await _context.GetAdditions<AdditionModel>();
                List<AdditionModel> additions = (List<AdditionModel>)AdditionResponse.Result;

                users.orders = orders;

                foreach (var item in orders)
                    item.orderDetails = ordersDetail.Where(x => x.IdOrder == item.IdOrder).ToList();

                foreach (var item in ordersDetail)
                {
                    item.OrdersDetailAdditions = ordersDetailAddition.Where(x => x.IdorderDetail == item.IdDetail).ToList();
                    item.foods = foods.Where(x => x.IdFood == item.IdFood).ToList();
                }

                foreach (var item in ordersDetailAddition)
                    item.additions = additions.Where(x => x.IdAddition == item.IdAddition).ToList();


                //List<FoodModel> foods = new List<FoodModel>();
                //List<AdditionModel> Additions = new List<AdditionModel>();

                //var orderReturn = (from O in orders 
                //                   join D in ordersDetail on O.IdOrder equals D.IdOrderDetail
                //                   join DA in ordersDetailAddition on D.IdOrderDetailAdditions equals DA.IdOrderDetailAddition
                //                   select new {  
                //                        O.IdOrder,
                //                        O.NOrder,
                //                        D.IdDetail,
                //                        D.IdFood,
                //                        D.DescriptionFood,
                //                        D.ImageId,
                //                        D.ImageFullPath,
                //                        D.Observations,
                //                        D.PriceFood,
                //                        DA.IdOrderDetailAddition,
                //                        DA.IdAddition,
                //                        DA.DescriptionAddition,
                //                        DA.PriceAddition
                //                   }).ToList();            
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("GetOrdersInfo")]
        //public async Task<IActionResult> GetOrdersInfo()
        //{
        //    try
        //    {
        //        Response response = await _context.GetOrdersInfo();
        //        List<OrderModel> foodModel = (List<OrderModel>)response.Result;

        //        return Ok(foodModel);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
