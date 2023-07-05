﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
