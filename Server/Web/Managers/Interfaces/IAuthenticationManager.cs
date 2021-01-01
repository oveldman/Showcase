using System;
using Web.Models.Authentication;

namespace Web.Managers.Interfaces
{
    public interface IAuthenticationManager
    {
        BearerViewModel Authenticate(string username, string password);
    }
}
