using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace OWIN.Windsor.DependencyResolverScopeMiddleware
{
    public static class Extensions
    {
        public static IAppBuilder UseWindsorDependencyResolverScope(this IAppBuilder app, HttpConfiguration config, IWindsorContainer container)
        {
            var windsorResolver = new DependencyResolver(container);
            config.DependencyResolver = windsorResolver;
            return app.Use<Middleware>(windsorResolver);
        }

        private const string DEPENDENCY_RESOLVER_KEY = "IDependencyResolver";

        public static void SetDependencyResolver(this IOwinContext ctx, IDependencyResolver resolver)
        {
            ctx.Environment[DEPENDENCY_RESOLVER_KEY] = resolver;
        }

        public static IDependencyResolver GetDependencyResolver(this IOwinContext ctx)
        {
            return ctx.Environment[DEPENDENCY_RESOLVER_KEY] as IDependencyResolver;
        }

        public static TService GetService<TService>(this IDependencyResolver resolver) where TService : class
        {
            return resolver.GetService(typeof(TService)) as TService;
        }
    }
}
