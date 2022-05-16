using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNotes.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundoosContext fundoosContext;
        IUserBL userBL;
        public UserController(FundoosContext fundoos, IUserBL userBL)
        {
            this.fundoosContext = fundoos;
            this.userBL = userBL;
        }

        [HttpPost("register")]

        public IActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"User Added Successful " });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        
        [HttpPost("login/{email}/{password}")]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {

                string token = this.userBL.LoginUser(email, password);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, message = $"Email or Password is invalid" });
                }
                return this.Ok(new { success = true, message = $"Token Generated is" + token });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword/{Email}")]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                bool result = this.userBL.ForgotPassword(Email);
                if (result)
                    return this.Ok(new { success = true, message = $"Email sent" });
                return this.BadRequest(new { success = false, message = $"Email does not exist" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = $"Password Reset Failed {e.Message}" });
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]

        public IActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                bool result = userBL.ChangePassword(changePassword, email);
                if (result == false)
                {

                    return this.BadRequest(new { success = false, message = "password not changed sucessfully" });
                }
                return this.Ok(new { success = true, message = "password changed succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpGet("getalluser")]
        public ActionResult GetAllUser()
        {
            try
            {
                var result = userBL.GetAllUser();
                return this.Ok(new { success = true, message = $"Here is the details of all user", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

        



    

