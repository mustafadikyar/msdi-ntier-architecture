using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.Abstract
{
    public interface  IUserService
    {
        List<OperationClaimDTO> GetClaims(User user);
        void Add(User user);
        User GetByMail(string mailAddress);
    }
}
