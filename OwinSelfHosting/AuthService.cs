using NHibernate;
using OwinSelfHosting.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace OwinSelfHosting
{
    public class AuthService : OwinSelfHosting.IAuthService
    {
        private readonly ISession _session;

        public AuthService(ISession session)
        {
            _session = session;
        }

        public IPrincipal Authenticate(string userName)
        {
            var users = _session.QueryOver<User>().List();

            var validUserLogins = users.Select(x => x.Login);

            if (!validUserLogins.Contains(userName, StringComparer.InvariantCultureIgnoreCase))
            {
                return null;
            }

            //Console.WriteLine(_session.GetSessionImplementation().SessionId);
            return new GenericPrincipal(new GenericIdentity(userName), new string[] { "guest" });
        }
    }
}
