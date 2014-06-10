using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace OwinSelfHosting.Extensions
{
    public static class DependencyResolverExtensions
    {
        public static TService GetService<TService>(this IDependencyResolver resolver) where TService : class
        {
            return resolver.GetService(typeof(TService)) as TService;
        }
    }
}
