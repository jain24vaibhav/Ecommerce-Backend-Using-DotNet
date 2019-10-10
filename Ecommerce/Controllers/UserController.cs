using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DA;
using Ecommerce.models;
using Ecommerce.models.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        EcommerceContext _db = new EcommerceContext();
        UserDA _user = new UserDA();

        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }



        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.userEmail),
                    new Claim(ClaimTypes.Role, user.userRole)
                };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Hash(string input)
        {
            return Convert.ToBase64String
                (
                 SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(input))
                );
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var res = _user.GetAllUsers();
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult UserLogin(LoginModel user)
        {
            user.userPassword = Hash(user.userPassword);
            var isUser = _user.UserLogin(user);
            if (isUser != null)
            {
                var tokenstring = GenerateJSONWebToken(isUser);
                return Ok( new { token = tokenstring, UserId = isUser.userId});
            }           
            else
                return BadRequest(new { message = "Invalid Email/Password" });
        }

        [HttpPost("register")]
        public IActionResult UserRegister(User user)
        {
            if (_user.IsUserByEmail(user.userEmail))
            {
                return BadRequest(new { message = "User alreadt exist." });
            }
            else
            {
                user.userRole = "User";
                user.userPassword = Hash(user.userPassword);
                var res = _user.UserRegister(user);
                if (res)
                {
                    var isUser = _user.GetUser(user.userEmail);
                    var tokenstring = GenerateJSONWebToken(isUser);
                    return Ok(new { token = tokenstring, UserId = isUser.userId });                 
                }
                else
                {
                    return BadRequest(new { message = "Error, can't register" });
                }
            }
            
        }

        [HttpPost("addtocart")]
        public IActionResult AddToCart(Cart cart)
        {
            var res = _user.AddToCart(cart);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Can't add to cart" });
            }
        }

        [HttpPost("isincart")]
        public IActionResult IsInCart(Cart cart)
        {
            var res = _user.IsInCart(cart);
            if (res)
                return Ok(new { status = true });
            else
                return Ok(new { status = false });
        }

        [HttpPost("removefromcart")]
        public IActionResult RemoveFromCart(Cart cart)
        {
            var res = _user.RemoveFromCart(cart);
            if (res)
            {
                return Ok(new { message = "Successfull removed form cart" });

            }
            else
            {
                return BadRequest(new { message = "Can't remove from cart" });
            }
        }

        [HttpGet("getcartitemsofuser/{id}")]
        public IActionResult GetCartItemsOfUser(int id)
        {
            var res = _user.GetCartItemsOfUser(id);
            return Ok(res);
        }

        [HttpPost("placeorder")]
        public IActionResult PlaceOrder(Address address)
        {
            var itemsInCart = _user.GetCartItemsOfUser(address.userId);
            if(itemsInCart!=null)
            {
                var obj = new order
                {
                    userId = address.userId,
                    orderDate = DateTime.Today.Date
                };
                var generateOrderId = _user.GenerateOrderId(obj);
                if (generateOrderId != -1)
                {               
                    foreach(var pro in itemsInCart)
                    {
                        var detailObj = new OrderDetail
                        {
                            orderId = generateOrderId,
                            productId = pro.productId
                        };
                        var rest = _user.AddToOrderDetail(detailObj);
                        if (rest)
                        {
                            continue;
                        }
                        else
                        {
                            BadRequest(new { message = "Error while adding to order detail" });
                        }
                    }

                    var res = _user.RemoveAllItemsOfUserFromCart(address.userId);
                    if(res)
                    {
                        address.orderId = generateOrderId;
                        var addAddress = _user.AddAddress(address);
                        if (addAddress)
                        {
                            return Ok(new { message = "Order placed successfully" });
                        }
                        else
                        {
                            return BadRequest(new { message = "Error while saving address" });
                        }
                        
                    }
                    else
                    {
                        return BadRequest(new { message = "Error while removing items fomr cart" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Not able to generate order id" });
                }
            }
            else
            {
                return BadRequest(new { message = "No item in cart" });
            }
        }

        [HttpGet("getorderhistory/{id}")]
        public IActionResult GetOrderHistory(int id)
        {
            var res = _user.GetOrderHistory(id);
            return Ok(res);
        }

    }
}