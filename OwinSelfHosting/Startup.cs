using Castle.Windsor;
using Castle.MicroKernel.Lifestyle;
using Owin;
using OwinSelfHosting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using System.Security.Principal;
using System.Web.Http.Dependencies;
using System.Web.Http;
using System.Net;
using NHibernate;
using Microsoft.Owin.Diagnostics;
using OWIN.Windsor.DependencyResolverScopeMiddleware;

namespace OwinSelfHosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BootstrapContainer();            

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.EnableWindowsAuthentication();

            app.UseErrorPage(ErrorPageOptions.ShowAll)
                .UseWindsorDependencyResolverScope(config, container)
                .UseWebApi(config);
                
            //.Use(async (context, next) =>
            //{
            //    var ioc = context.Environment["resolver"] as IDependencyResolver;

            //    var helloService = ioc.GetService(typeof(IHelloService)) as IHelloService;

            //    helloService.SayHello();

            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync(string.Format("hello world! from {0}", context.Request.Path.Value));

            //    await next.Invoke();
            //});
        }

        private IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();

            //NHibernate
            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod((x, y) => { 
                return SessionFactoryHelper.GetSessionFactory(); 
            }));
            container.Register(Component.For<ISession>().UsingFactoryMethod((x, y) => { return x.Resolve<ISessionFactory>().OpenSession(); }).OnDestroy(session => { 
                session.Dispose(); 
            }).LifestyleScoped());

            
            container.Register(Component.For<IAuthService>().ImplementedBy<AuthService>().LifestyleScoped());
            container.Register(Component.For<IHelloService>().ImplementedBy<HelloService>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());


            return container;
        }
    }
}
