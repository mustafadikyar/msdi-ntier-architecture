using Msdi.Core.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Msdi.Entities.Concrete
{
    public partial class User : IEntity
    {
        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
