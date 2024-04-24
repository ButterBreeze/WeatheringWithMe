using BusinessLayer;
using Data.Repositories;
using System;

using Unity;

namespace Interface
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion


        public static void RegisterTypes(IUnityContainer container)
        {
            var weatherApiUri = System.Configuration.ConfigurationManager.
                ConnectionStrings["WeatherApi"].ConnectionString;
            var databaseConnString =  System.Configuration.ConfigurationManager
                .ConnectionStrings["DatabaseConnection"].ConnectionString;
            
            container.RegisterInstance<IForecastRepository>( new ForecastRepository(weatherApiUri));
            container.RegisterInstance<IDatabaseRepository>( new DatabaseRepository(databaseConnString));
            container.RegisterType<ProcessModel>();
        }
    }
}