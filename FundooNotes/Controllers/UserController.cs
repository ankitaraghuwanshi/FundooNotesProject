using BusinessLayer.Interfaces;
using CommonLayer.Users;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;

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
        [HttpPost]

        public IActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"User Added Successful " });



            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("login")]
        public IActionResult LoginUser(string email, string password)
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
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
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

    }
}
