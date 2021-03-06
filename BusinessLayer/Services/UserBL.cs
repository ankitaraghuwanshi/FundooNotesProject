using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Users;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL

    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void AddUser(UserPostModel user)
        {
            try
            {
                this.userRL.AddUser(user);

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
                return this.userRL.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }

        }

        

        public bool ChangePassword(ChangePasswordModel changePassword, string email)
        {
            try
            {
                return this.userRL.ChangePassword(changePassword,email);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetAllUser()
        {
            try
            {
                return userRL.GetAllUser();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
