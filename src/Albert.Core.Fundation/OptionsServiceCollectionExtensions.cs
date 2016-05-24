using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Albert.Core.Fundation
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class OptionsServiceCollectionExtensions
    {
        public static void InjectionProperty(this object obj, IConfigurationSection configurationSection)
        {
            var configures = configurationSection.GetChildren();
            foreach (var pro in obj.GetType().GetProperties())
            {
                foreach (var con in configures)
                {
                    if (pro.Name == con.Key)
                    {
                        pro.SetValue(obj, con.Value);
                    }
                }
            }
        }
        public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, IConfigurationSection configurationSection) where TOptions : class
        {
            return services.Configure<TOptions>((a) => { a.InjectionProperty(configurationSection); });
        }

        public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, IConfigurationRoot configuration, string configurationName) where TOptions : class
        {
            return services.Configure<TOptions>((a) => { a.InjectionProperty(configuration.GetSection(configurationName)); });
        }

        public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, IConfigurationRoot configuration) where TOptions : class
        {
            var configName = typeof(TOptions).Name;
            return services.Configure<TOptions>((a) => { a.InjectionProperty(configuration.GetSection(configName)); });
        }
    }
}
