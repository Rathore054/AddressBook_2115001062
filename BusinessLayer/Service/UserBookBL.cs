using AutoMapper;
using BusinessLayer.Interface;
using BusinessLayer.Middleware.Salting;
using BusinessLayer.Middleware.Authenticator;
//using Middleware.Authenticator;
//using Middleware.Salting;

using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBookBL : IUserBookBL
    {
        private readonly IUserBookRL _userRepository;
        private readonly JWTToken _jwtTokenService;
        private readonly IMapper _mapper;

        public UserBookBL(IUserBookRL userRepository, JWTToken jwtTokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public bool RegisterUser(RegisterUser request)
        {
            try
            {
                // Check if the user already exists
                var existingUser = _userRepository.GetUserByEmail(request.Email);
                if (existingUser != null)
                    return false; // User already exists

                // Map DTO to Entity
                var User = _mapper.Map<UserEntity>(request);
                User.PasswordHash = PasswordHelp.HashPassword(request.Password);

                _userRepository.AddUser(User);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RegisterUser] Error: {ex.Message}");
                return false;
            }
        }

        public string? LoginUser(UserLogin request)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(request.Email);
                if (user == null || !PasswordHelp.VerifyPassword(request.Password, user.PasswordHash))
                    return null;
                var userModel = _mapper.Map<UserModel>(user);

                return _jwtTokenService.GenerateToken(userModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoginUser] Error: {ex.Message}");
                return null;
            }
        }
    }
}