using Msdi.Core.DataAccess;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaimDTO> GetClaims(User user);
    }
}
