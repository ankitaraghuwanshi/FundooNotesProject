using CommonLayer;
using CommonLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string Email, string Password);
        public bool ForgotPassword(string email);
        public bool ChangePassword(ChangePasswordModel changePassword, string email);
        List<User> GetAllUser();

    }
}
