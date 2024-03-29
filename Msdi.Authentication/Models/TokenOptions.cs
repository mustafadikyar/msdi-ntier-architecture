﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Authentication.Models
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
