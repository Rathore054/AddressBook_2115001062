﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserBookRL : IUserBookRL
    {
        private readonly ContextRL _context;

        public UserBookRL(ContextRL context)
        {
            _context = context;
        }

        public UserEntity? GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetUserByEmail] Error: {ex.Message}");
                return null;
            }
        }

        public void AddUser(UserEntity user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AddUser] Error: {ex.Message}");
            }
        }
        public void UpdateUser(UserEntity user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}