using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KBPL.DllInjector
{
    public class ServiceRegistrationManager
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
      
            services.AddSingleton(_ => configuration);

            //APP Setting configuration
            //services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // BusinessManagerDIInjector.InjectDependencies(services, configuration, azureKeyVaultValues);

            //services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.TryAddTransient<IUserIdentityService, UserIdentityService>();
            //services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(StackExchange.Redis.ConnectionMultiplexer.Connect(azureKeyVaultValues.RedisConnectionConnectionString));
            //services.AddSingleton<ICacheHelper, RedisCacheHelper>();
            //services.AddScoped<IJwtClaimsManager, JwtClaimsManager>();
            //services.AddScoped<IB2CGraphClient, B2CGraphClient>();

            //var confg = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new ViewModelToEntityMappingProfile());
            //}
            //);
            //IMapper mapper = confg.CreateMapper();
            //services.AddSingleton(mapper);
        }
    }
}
