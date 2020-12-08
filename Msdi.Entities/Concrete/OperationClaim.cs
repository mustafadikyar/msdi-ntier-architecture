using Msdi.Core.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Msdi.Entities.Concrete
{
    public partial class OperationClaim : IEntity
    {
        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int OperationClaimId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
