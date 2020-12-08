using Msdi.Core.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace Msdi.Entities.Concrete
{
    public partial class UserOperationClaim : IEntity
    {
        public int UserOperationClaimId { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public virtual OperationClaim OperationClaim { get; set; }
        public virtual User User { get; set; }
    }
}
