using Msdi.Core.DataAccess.EntityFramework;
using Msdi.DataAccess.Abstract;
using Msdi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Msdi.ViewModels.DTOs.Authentication;

namespace Msdi.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EFEntityRepositoryBase<User, Msdi_2020_DbContext>, IUserDal
    {
        public List<OperationClaimDTO> GetClaims(User user)
        {
            using (Msdi_2020_DbContext db = new Msdi_2020_DbContext())
            {
                var result = from operationClaim in db.OperationClaims
                             join userOperationClaim in db.UserOperationClaims
                                 on operationClaim.OperationClaimId equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaimDTO { OperationClaimId = operationClaim.OperationClaimId, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
