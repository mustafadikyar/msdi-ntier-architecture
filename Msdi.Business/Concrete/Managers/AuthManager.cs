using AutoMapper;
using Msdi.Authentication.Abstract;
using Msdi.Authentication.Helpers;
using Msdi.Authentication.Models;
using Msdi.Business.Abstract;
using Msdi.Entities.Concrete;
using Msdi.Exceptions.Abstract;
using Msdi.Exceptions.Results;
using Msdi.ViewModels.Constants.Messages;
using Msdi.ViewModels.DTOs.Authentication;
using System;
using System.Collections.Generic;

namespace Msdi.Business.Concrete.Managers
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, _mapper.Map<List<OperationClaimDTO>, List<OperationClaim>>(claims));
            return new SuccessDataResult<AccessToken>(accessToken, AuthMessages.AccessTokenCreated);
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
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, AuthMessages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(AuthMessages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
