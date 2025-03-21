﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ModelLayer.Model;
//using RepositoryLayer.Service;

namespace AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBookBL _userService;

        public AuthController(IUserBookBL userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUser request)
        {
            bool isRegistered = _userService.RegisterUser(request);
            if (!isRegistered)
                return BadRequest("User with this email already exists.");

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            var token = _userService.LoginUser(request);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            bool isSent = _userService.ForgotPassword(request.Email);
            if (!isSent)
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email not found." });

            return Ok(new ResponseModel<string> { Success = true, Message = "Reset password email sent successfully." });
        }

        [HttpGet("reset-password")]
        public ContentResult ResetPasswordForm([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return new ContentResult { Content = "Token is required.", ContentType = "text/plain" };

            string htmlForm = $@"
                <html>
                <body>
                     <form action='/api/Auth/reset-password-form' method='post'>
                        <input type='hidden' name='token' value='{token}' />
                        <label>New Password:</label>
                        <input type='password' name='newPassword' required />
                        <button type='submit'>Reset Password</button>
                    </form>
                </body>
                 </html>";

            return new ContentResult { Content = htmlForm, ContentType = "text/html" };
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Token and new password are required." });

            bool isResetSuccessful = _userService.ResetPassword(request.Token, request.NewPassword);
            if (!isResetSuccessful)
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid or expired token." });

            return Ok(new ResponseModel<string> { Success = true, Message = "Password reset successfully." });
        }

        [HttpPost("reset-password-form")]
        public IActionResult ResetPasswordForm([FromForm] string token, [FromForm] string newPassword)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Token and new password are required." });

            bool isResetSuccessful = _userService.ResetPassword(token, newPassword);
            if (!isResetSuccessful)
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid or expired token." });

            return Ok(new ResponseModel<string> { Success = true, Message = "Password reset successfully." });
        }
    }
}