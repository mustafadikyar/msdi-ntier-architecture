using Msdi.Authentication.Models;
using Msdi.Entities.Concrete;
using Msdi.Exceptions.Abstract;
using Msdi.ViewModels.DTOs.Authentication;

namespace Msdi.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDTO model, string password);
        IDataResult<User> Login(UserForLoginDTO model);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
