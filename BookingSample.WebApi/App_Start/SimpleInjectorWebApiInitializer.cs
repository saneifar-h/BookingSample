using System.Web.Http;
using BookingSample.AppService.BookingSrv;
using BookingSample.DataAccess;
using BookingSample.Domain;
using BookingSample.Domain.Repositories;
using BookingSample.Domain.Validators;
using BookingSample.WebApi;
using BookingSample.WebApi.Models.AuthSrv;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

[assembly: WebActivator.PostApplicationStartMethod(typeof(SimpleInjectorWebApiInitializer), "Initialize")]

namespace BookingSample.WebApi
{
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IConfigurationLookup, ConfigurationLookup>(Lifestyle.Scoped);
            container.Register<ILogAdapter, LogAdapter>(Lifestyle.Scoped);
            container.Register<IConnectionStringProvider, SqlConnectionStringProvider>(Lifestyle.Scoped);
            container.Register<IBookingRepository, BookingRepository>(Lifestyle.Scoped);
            container.Register<IRoomRepository, RoomRepository>(Lifestyle.Scoped);
            container.Register<IBookingService, BookingService>(Lifestyle.Scoped);
            container.Register<IAddBookingValidator, AddBookingValidator>(Lifestyle.Scoped);
            container.Register<IAuthService, AuthService>(Lifestyle.Scoped);
        }
    }
}