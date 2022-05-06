﻿using CommonLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);

        public string LoginUser( string email, string password);
        
    }
    
}