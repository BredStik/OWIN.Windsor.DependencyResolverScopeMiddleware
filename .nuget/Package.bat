msbuild.exe "..\OWIN.Windsor.DependencyResolverScopeMiddleware\OWIN.Windsor.DependencyResolverScopeMiddleware.csproj" /p:Configuration=Release
nuget.exe pack "..\OWIN.Windsor.DependencyResolverScopeMiddleware\OWIN.Windsor.DependencyResolverScopeMiddleware.csproj" -Prop Configuration=Release

