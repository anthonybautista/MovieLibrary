using System;
using MovieLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MovieLibrary
{
    /// <summary>
    ///     Used for registration of new interfaces
    /// </summary>
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Add new lines of code here to register any interfaces and concrete services you create
            services.AddTransient<IMainService, MainService>();
            //services.AddTransient<IRepository, JsonRepository>();
            services.AddTransient<IController, DbController>();
            services.AddTransient<IMenu, ProgramMenus>();

            return services.BuildServiceProvider();
        }
    }
}