using ECommerceShop.DataAccess.Implementation;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Models.DTO;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUnitofWork _unitofWork;
        public UsersController (IUnitofWork unitofWork) 
        {
            _unitofWork = unitofWork;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User { UserName = newUser.userName, Email = newUser.email,PhoneNumber=newUser.phoneNumber};

            var result = await _unitofWork.User.CreateAsync(user, newUser.password);
            if (result.Succeeded)
            {
                //await _unitOfWork.CompleteAsync();
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn (LoginDto login)
        {
            if (ModelState.IsValid)
            {
                User user = await _unitofWork.User.FindByEmailAsync(login.userEmail);
                if(user != null)
                {
                    if( await _unitofWork.User.CheckPasswordAsync(user, login.password))
                    {
                        var token = _unitofWork.User.GenerateJwtToken(user);
                        return Ok(new { Token = token });
                    }
                }
            }

            return BadRequest();
        }
    }
}
