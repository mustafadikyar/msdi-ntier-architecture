using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Msdi.Core.CrossCuttingConcerns.Caching;
using Msdi.Core.CrossCuttingConcerns.Caching.Memcache;
using Msdi.Core.CrossCuttingConcerns.Caching.Microsoft;
using Msdi.Core.Utilities.IoC;
using System.Diagnostics;

namespace Msdi.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            //services.AddSingleton<ICacheManager, MemcacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
