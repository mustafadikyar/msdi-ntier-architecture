using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Msdi.Core.CrossCuttingConcerns.Caching;
using Msdi.Core.Utilities.Interceptors;
using Msdi.Core.Utilities.IoC;

namespace Msdi.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
