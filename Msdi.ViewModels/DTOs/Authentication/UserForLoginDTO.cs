using Msdi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.ViewModels.DTOs.Authentication
{
    public class UserForLoginDTO : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
