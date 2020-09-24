﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using ApiTask.Model;
using ApiTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Repository
{
    public class Registration : IRegistration
    {
        private ApiTaskDBContext _context;
        private IEmailSender _emailSender;

        public Registration(ApiTaskDBContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<bool> RegisterUser(RegisterUsers registerUsers)
        {
            var usercount = await _context.RegisterUser.Where(user => user.Username == registerUsers.Username
                                                            && user.Password == PasswordEncryption.HashPassword(registerUsers.Password)
                                                            && user.ApiKey == registerUsers.ApiKey
                                                            && user.Email == registerUsers.Email)
                                                       .CountAsync();

            if (usercount > 0)
            {
                return true;
            }
            else
            {
                var user = new RegisterUsers
                {
                    Username = registerUsers.Username,
                    Password = PasswordEncryption.HashPassword(registerUsers.Password),
                    Email = registerUsers.Email,
                    ApiKey = registerUsers.ApiKey,
                    SignupDate = DateTime.Now
                };

                await _context.RegisterUser.AddAsync(user);
                await _context.SaveChangesAsync();
                return false;
            }
        }

        public async Task<string> SendPasswordResetLinkEmail(string email, string link)
        {
            var user = await _context.RegisterUser.Where(user => user.Email == email).FirstOrDefaultAsync();

            if (user != null)
            { 
                string subject = "Reset Password";
                string message = "Hi there,<br/><br/>We got a request for resetting your account password. Please click the link below to reset your password." 
                                  + "<br/><br/>" + "<a href=" + link + ">Reset Password Link</a>";

                await _emailSender.SendEmailAsync(email, subject, message);

                return "Password reset link has been sent to your email...";
            }
            else
            {
                return "User not found...";
            }
        }

        public async Task<string> ResetUserPassword(ResetPassword resetPassword)
        {
            var user = await _context.RegisterUser.Where(user => user.Email == resetPassword.Email)
                                            .FirstOrDefaultAsync();

            if(user != null)
            {
                user.Password = PasswordEncryption.HashPassword(resetPassword.NewPassword);
                await _context.SaveChangesAsync();

                return "New password updated successfully...";
            }
            else
            {
                return "Account doesn't exist...";
            }
        }
    }
}
