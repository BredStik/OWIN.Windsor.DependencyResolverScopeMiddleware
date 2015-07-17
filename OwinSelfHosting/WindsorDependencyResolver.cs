using Castle.Windsor;
using Castle.MicroKernel.Lifestyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Threading;

namespace OwinSelfHosting
{
    public class WindsorDependencyResolver: IDependencyResolver, IServiceProvider
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            Console.WriteLine("Resolving component of type: {0}", serviceType.Name);
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            Console.WriteLine("Resolving component of type: {0}", serviceType.Name);
            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {}
    }
}
