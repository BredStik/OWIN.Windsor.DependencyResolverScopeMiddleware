using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace OwinSelfHosting.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseDependencyResolverScope(this IAppBuilder app, IDependencyResolver resolver)
        {
            return app.Use<DependencyResolverScopeMiddleware>(resolver);
        }

        public static IAppBuilder UseAuth(this IAppBuilder app)
        {
            return app.Use<AuthMiddleware>();
        }

        public static void EnableWindowsAuthentication(this IAppBuilder app)
        {
            var listener = app.Properties["System.Net.HttpListener"] as HttpListener;
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;
        }
    }
}
