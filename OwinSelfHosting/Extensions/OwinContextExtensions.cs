using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace OwinSelfHosting.Extensions
{
    public static class OwinContextExtensions
    {
        private const string DEPENDENCY_RESOLVER_KEY = "IDependencyResolver";

        public static void SetDependencyResolver(this IOwinContext ctx, IDependencyResolver resolver)
        {
            ctx.Environment[DEPENDENCY_RESOLVER_KEY] = resolver;
        }

        public static IDependencyResolver GetDependencyResolver(this IOwinContext ctx)
        {
            return ctx.Environment[DEPENDENCY_RESOLVER_KEY] as IDependencyResolver;
        }
    }
}
