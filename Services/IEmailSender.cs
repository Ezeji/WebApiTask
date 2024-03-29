﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task Execute(string apiKey, string subject, string message, string email);
    }
}
