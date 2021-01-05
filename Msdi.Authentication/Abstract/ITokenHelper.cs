using Msdi.Authentication.Models;
using Msdi.Entities.Concrete;
using System.Collections.Generic;

namespace Msdi.Authentication.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> claims);
    }
}
