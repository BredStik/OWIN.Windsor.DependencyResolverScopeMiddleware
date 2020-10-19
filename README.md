# OWIN.Windsor.DependencyResolverScopeMiddleware
Creates a Windsor dependency scope per OWIN WebAPI request allowing to use a per request lifestyle component registration in a self-host environment.

# Usage

1.  Add the "OWIN.Windsor.DependencyResolverScopeMiddleware" nuget package to your OWIN self-hosted WebAPI project.
2.  In your Startup.cs file, add "using OWIN.Windsor.DependencyResolverScopeMiddleware;" and configure your Windsor container.
3.  Configure any "per request" component using the "LifestyleScoped" lifestyle.
4.  Use the following line to configure you app by passing in the HttpConfiguration and the container (where app: IAppBuilder): app.UseWindsorDependencyResolverScope(config, container).UseWebApi(config);


Your scoped components will by created once per request and disposed at the end of it automatically.

If you ever need to use the dependency resolver inside a subsequent middleware, just use the "GetDependencyResolver" extension method on the IOwinContext from the "OWIN.Windsor.DependencyResolverScopeMiddleware" namespace.

