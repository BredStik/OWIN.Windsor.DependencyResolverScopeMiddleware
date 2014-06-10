using System;
namespace OwinSelfHosting
{
    public interface IAuthService
    {
        System.Security.Principal.IPrincipal Authenticate(string userName);
    }
}
