using Msdi.Authentication.Helpers;
using Msdi.Authentication.Models;
using Msdi.Business.Abstract;
using Msdi.Entities.Concrete;
using Msdi.Exceptions.Abstract;
using Msdi.Exceptions.Results;
using Msdi.ViewModels.Constants.Messages;
using Msdi.ViewModels.DTOs.Authentication;
using System;

namespace Msdi.Business.Concrete.Managers
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Login(UserForLoginDTO model)
        {
            var userExists = _userService.GetByMail(model.Email);
            if (userExists == null)
                return new ErrorDataResult<User>(message: AuthMessages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(model.Password, userExists.PasswordHash, userExists.PasswordSalt))
                return new ErrorDataResult<User>(message: AuthMessages.PasswordError);

            return new SuccessDataResult<User>(userExists, AuthMessages.Success);
        }

        public IDataResult<User> Register(UserForRegisterDTO model, string password)
        {
            throw new NotImplementedException();
        }

        public IResult UserExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}
