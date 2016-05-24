using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Albert.Core.Fundation.RC2
{

    /// <summary>
    /// .Net Core RC2 OptionsServiceCollectionExtensions with myself
    /// </summary>
    public static class OptionsServiceCollectionExtensions_Self
    {
        /// <summary>
        /// 为<see cref="obj"/>注入配置信息
        /// </summary>
        /// <param name="obj">需要注入属性的字段</param>
        /// <param name="configurationSection">配置节点</param>
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
