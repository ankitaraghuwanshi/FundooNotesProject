using CommonLayer;
using CommonLayer.Users;
using Experimental.System.Messaging;
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
            string passwordToEncript = string.Empty;
            try
            {
                User userdata = new User();
                userdata.firstName = user.firstName;
                userdata.lastName = user.lastName;
                userdata.email = user.email;
                passwordToEncript = EncodePasswordToBase64(user.password);
                userdata.password = passwordToEncript;
                fundoosContext.Add(userdata);
                fundoosContext.SaveChanges();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string LoginUser(string email, string password)
        {
            try
            {
               
                var AllRecords = fundoosContext.User.ToList();
                var existingRecord = AllRecords.Where(x => x.email == email).FirstOrDefault();

                if (existingRecord != null)
                {
                    var decriptedPassword = DecodeFrom64(existingRecord.password);
                    bool conditionCheck = decriptedPassword == password ? true : false;
                    if (conditionCheck == false)
                    {
                        return "Invalid credentials";
                    }
                    else
                    {
                        return GenerateJWTToken(existingRecord.email, existingRecord.userId);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        public bool ForgotPassword(string email)
        {
            try
            {
                var user = fundoosContext.User.FirstOrDefault(u => u.email == email);
                if (user == null)
                    return false;
                MessageQueue FundooQ;
                //Add message to queue
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    FundooQ = new MessageQueue(@".\Private$\FundooQueue");
                else FundooQ = MessageQueue.Create(@".\Private$\FundooQueue");

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GenerateJWTToken(email, user.userId);
                EmailService.SendMail(email, message.Body.ToString());
                FundooQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                };
            }
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email)
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


        //this function Convert to Encord your Password 
        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public bool ChangePassword(ChangePasswordModel changePassword, string email)
        {
            try
            {
                var user = fundoosContext.User.FirstOrDefault(u => u.email == email);
                if (changePassword.password.Equals(changePassword.conformPassword))
                {
                    var password = EncodePasswordToBase64(changePassword.conformPassword);
                    user.password = password;
                    fundoosContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}
