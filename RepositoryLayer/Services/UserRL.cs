using CommonLayer.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get; }
            public UserRL(FundoosContext fundoosContext,IConfiguration configuration) 
            {
                  this.fundoosContext = fundoosContext;
                  this.Configuration = Configuration;
            }

        public void AddUser(UserPostModel user)
        {
            try
            {
                User userdata = new User();
                userdata.firstName = user.firstName;
                userdata.lastName = user.lastName;
                userdata.email = user.email;
                userdata.password = user.password;
                fundoosContext.Add(userdata);
                fundoosContext.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                var user = fundoosContext.User.FirstOrDefault(u => u.email == email && u.password == password);
                if(user == null)
                {
                    return null;


                }
                return GenerateJWTToken(email, user.Id);

            }
            catch (Exception)
            {

                throw;
            }
              
        }
        private string GenerateJWTToken(string email, int userId)
        {
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
