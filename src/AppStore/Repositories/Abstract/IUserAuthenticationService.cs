using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status>LoginAsync(LoginModel login);
        Task LogoutAsync();
    }
}