using Msdi.Business.Abstract;
using Msdi.DataAccess.Abstract;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string mailAddress)
        {
            return _userDal.Get(c => c.Email.Equals(mailAddress));
        }

        public List<OperationClaimDTO> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
    }
}
