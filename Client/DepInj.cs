using Client.Interfaces;
using Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Client
{
    public static class DepInj
    {
        public static void RegisterRedisCacheClient(this IServiceCollection services, Action<RedisCacheClientOptions> configureClientOptions)
        {
            services.ConfigureServiceOptions<RedisCacheClientOptions>((_, opt) => configureClientOptions(opt));
            services.ConnectToRedisCacheServer();
            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
        }
        
        private static void ConfigureServiceOptions<TOptions>(
            this IServiceCollection services,
            Action<IServiceProvider, TOptions> configure)
            where TOptions : class
        {
            services
                .AddOptions<TOptions>()
                .Configure<IServiceProvider>((options, resolver) => configure(resolver, options));
        }

        private static void ConnectToRedisCacheServer(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var option = sp.GetRequiredService<IOptions<RedisCacheClientOptions>>();
                
                var cfg = new ConfigurationOptions
                {
                    Password = option.Value.Password,
                    Ssl = option.Value.Ssl,
                    AbortOnConnectFail = option.Value.AbortOnConnectFail,
                };
                cfg.EndPoints.Add(option.Value.Host);


                return ConnectionMultiplexer.Connect(cfg);
            });
        } 
    }
}