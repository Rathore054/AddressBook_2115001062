﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserBookRL
    {
        public UserEntity? GetUserByEmail(string email);
        public void AddUser(UserEntity user);
        public void UpdateUser(UserEntity user);

    }
}