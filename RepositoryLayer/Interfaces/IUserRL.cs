using CommonLayer;
using CommonLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser( string email, string password);
        public bool ForgotPassword(string email);
        public bool ChangePassword(ChangePasswordModel changePassword, string email);
        List<User> GetAllUser();

    }
    
}
