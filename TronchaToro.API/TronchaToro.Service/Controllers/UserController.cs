using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IUserHelper _userHelper;

        public UserController(IData context, ITokenHelper tokenHelper, IUserHelper userHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _userHelper = userHelper;
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
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                Response response = await _context.GetUsers<UserModel>();
                List<UserModel> foodModel = (List<UserModel>)response.Result;

                return Ok(foodModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrdersInfo/{email}/{State}")]
        public async Task<IActionResult> GetOrdersInfo(string email, int State)
        {
            try
            {
                Response userResponse = await _context.GetUser<UserModel>(email);
                UserModel users = (UserModel)userResponse.Result;

                Response OrdersResponse = await _context.GetOrders<OrderModel>(email, State);
                List<OrderModel> orders = (List<OrderModel>)OrdersResponse.Result;

                Response OrdersDetailResponse = await _context.GetOrdersDetail<OrderDetailsModel>(email, State);
                List<OrderDetailsModel> ordersDetail = (List<OrderDetailsModel>)OrdersDetailResponse.Result;

                Response OrdersDetailAdditionsResponse = await _context.GetOrdersDetailAdditions<OrdersDetailAdditionModel>(email, State);
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
   
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderRequest Order)
        {
            try
            {
                if (Order.FullName != "")
                {
                    Response responseOrder = await _context.AddOrder<int>(Order);
                    if (!responseOrder.IsSuccess)
                        return BadRequest(responseOrder.Message);

                    int order = (int)responseOrder.Result;
                    Order.orderDetails.IdOrder = order;
                }

                Response responseOrderDetails = await _context.AddOrderDetail<int>(Order.orderDetails);
                if (!responseOrderDetails.IsSuccess)
                    return BadRequest(responseOrderDetails.Message);

                int idOrderDetail = (int)responseOrderDetails.Result;

                List<UOrderDetailsAdditionsRequest> orderDetailsAdditionsRequests = 
                    Order.orderDetails.OrderDetailAdditions.Select(x => new UOrderDetailsAdditionsRequest { IdAddition = x.IdAddition, Quantity = x.Quantity, Id = x.Id, IdDetailOrder = idOrderDetail }).ToList();
                if (orderDetailsAdditionsRequests != null)
                {
                    Response responseOrderDetailsAdditions = await _context.UpdateOrderDetailAdditions<Response>(orderDetailsAdditionsRequests);
                    if (!responseOrderDetailsAdditions.IsSuccess)
                        return BadRequest(responseOrderDetailsAdditions.Message);
                }

                return Ok("Datos guardados con éxito");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] //[HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderDetailRequest OrderDetail)
        {
            try
            {
                Response responseOrderDetails = await _context.UpdateOrderDetail<Response>(OrderDetail);
                if (!responseOrderDetails.IsSuccess)
                    return BadRequest(responseOrderDetails.Message);

                List<UOrderDetailsAdditionsRequest> orderDetailsAdditionsRequests = OrderDetail.OrderDetailAdditions.Select(x => new UOrderDetailsAdditionsRequest { IdAddition = x.IdAddition, Quantity = x.Quantity, Id = x.Id, IdDetailOrder = x.IdDetailOrder }).ToList();
                if (orderDetailsAdditionsRequests != null)
                {
                    Response responseOrderDetailsAdditions = await _context.UpdateOrderDetailAdditions<Response>(orderDetailsAdditionsRequests);
                    if (!responseOrderDetailsAdditions.IsSuccess)
                        return BadRequest(responseOrderDetailsAdditions.Message);
                }
                

                return Ok("Datos actualizados con éxito");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] //[HttpDelete]
        [Route("DeleteOrderDtl/{id}")]
        public async Task<IActionResult> DeleteOrderDtl(int id)
        {
            try
            {
                Response responseOrderDetails = await _context.DeleteOrderDetail(id);
                if (!responseOrderDetails.IsSuccess)
                    return BadRequest(responseOrderDetails.Message);

                return Ok("Datos Borrados con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SocialLogin")]
        public async Task<IActionResult> SocialLogin(RegisterUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Usuario o contraseña son incorrectos");

                Response userResponse = await _context.GetUser<UserModel>(model.Email);
                UserModel users = (UserModel)userResponse.Result;
                if (users != null)
                {
                    if (users.LoginType != model.LoginType)
                    {
                        return BadRequest("El usuario ya inició sesión previamente por email o por otra red social");
                    }

                    //string contraseña = _tokenHelper.cifrarMD5(model.Password);
                    //model.Password = contraseña;

                    //Response responseValid = await _context.GetValidUser(new LoginRequest {Email = model.Email, Contraseña = contraseña});
                    //if (responseValid.Result == null)
                    //    return BadRequest("Usuario o contraseña son incorrectos");

                    await _userHelper.UpdateUserAsync(model);

                    object token = _tokenHelper.CrearToken(users);
                    return Created(string.Empty, token);
                }
                else
                {
                    await _userHelper.CreateUserAsync(model);
                    Response userResponse2 = await _context.GetUser<UserModel>(model.Email);
                    UserModel users2 = (UserModel)userResponse2.Result;
                    object token = _tokenHelper.CrearToken(users2);
                    return Created(string.Empty, token);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected string cifrarMD5(string password)
        {
            MD5CryptoServiceProvider encriptador = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(password);
            bs = encriptador.ComputeHash(bs);

            string passHash = null;

            foreach (byte b in bs)
            {
                passHash += b.ToString("x2").ToLower();
            }

            string clave_cifrada = passHash;
            return clave_cifrada;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUsere(RegisterUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                model.Password = cifrarMD5(model.Password);

                await _userHelper.CreateUserAsync(model);
                Response userResponse = await _context.GetUser<UserModel>(model.Email);
                UserModel users = (UserModel)userResponse.Result;
                object token = _tokenHelper.CrearToken(users);
                return Created(string.Empty, token);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(RegisterUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _userHelper.UpdateUserAsync(model);
                Response userResponse = await _context.GetUser<UserModel>(model.Email);
                UserModel users = (UserModel)userResponse.Result;
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
