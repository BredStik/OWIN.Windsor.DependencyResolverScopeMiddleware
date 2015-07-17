using Castle.Windsor;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace OWIN.Windsor.DependencyResolverScopeMiddleware
{
    public class Middleware: OwinMiddleware
    {
        private readonly IDependencyResolver _resolver;

        public Middleware(OwinMiddleware next, IDependencyResolver resolver)
            : base(next)
        {
            _resolver = resolver;
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.SetDependencyResolver(_resolver);
            using (var scope = _resolver.BeginScope())
            {
                await Next.Invoke(context);
            }
        }
    }
}
