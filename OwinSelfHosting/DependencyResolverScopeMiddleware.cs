using Castle.Windsor;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using OwinSelfHosting.Extensions;

namespace OwinSelfHosting
{
    public class DependencyResolverScopeMiddleware: OwinMiddleware
    {
        private readonly IDependencyResolver _resolver;

        public DependencyResolverScopeMiddleware(OwinMiddleware next, IDependencyResolver resolver)
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
