using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Threading;
using System.Web.Http.Dependencies;
using System.Net;
using System.IO;
using OwinSelfHosting.Extensions;
using NHibernate;
using OwinSelfHosting.Entities;

namespace OwinSelfHosting
{
    public class AuthMiddleware: OwinMiddleware
    {
        public AuthMiddleware(OwinMiddleware next)
        : base(next)
        {}

        public override async Task Invoke(IOwinContext context)
        {
            var resolver = context.GetDependencyResolver();
            var authService = resolver.GetService<IAuthService>();

            //System.Runtime.Caching
            var authenticatedUser = authService.Authenticate(context.Request.User.Identity.Name);

            if(authenticatedUser == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                using (var sw = new StreamWriter(context.Response.Body))
                {
                    await sw.WriteAsync("You are unauthorized to access this resource.");
                }
                return;
            }

            context.Request.User = authenticatedUser;
            Thread.CurrentPrincipal = context.Request.User;
            
            await Next.Invoke(context);
        }
    }
}
