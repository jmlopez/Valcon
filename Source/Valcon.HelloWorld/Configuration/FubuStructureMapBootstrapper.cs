using System.Web.Routing;
using AutoMapper;
using FubuCore;
using FubuMVC.StructureMap;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using Valcon.HelloWorld.Configuration.Registries;
using Valcon.HelloWorld.Configuration.Validation;
using Valcon.HelloWorld.Domain;
using Valcon.HelloWorld.Models.Users;

namespace Valcon.HelloWorld.Configuration
{
    public class FubuStructureMapBootstrapper : IBootstrapper
    {
        private readonly RouteCollection _routes;

        private FubuStructureMapBootstrapper(RouteCollection routes)
        {
            _routes = routes;
        }

        public void BootstrapStructureMap()
        {
            UrlContext.Reset();

            ObjectFactory.Initialize(x => x.IncludeRegistry(new CoreRegistry()));

            Mapper.Initialize(x => x.CreateMap<User, UserDetailsModel>());

            Validator.Initialize(x =>
                                     {
                                         x.AddRegistry<CoreValidationRegistry>();
                                         x.BuildDependenciesWith(ObjectFactory.GetInstance);
                                     });

            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(ObjectFactory.Container));

            BootstrapFubu(ObjectFactory.Container, _routes);
        }

        private static void BootstrapFubu(IContainer container, RouteCollection routes)
        {
            var bootstrapper = new StructureMapBootstrapper(container, new HelloWorldFubuRegistry());
            bootstrapper.Bootstrap(routes);
        }

        public static void Bootstrap(RouteCollection routes)
        {
            new FubuStructureMapBootstrapper(routes).BootstrapStructureMap();
        }
    }
}