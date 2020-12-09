using Microsoft.Extensions.DependencyInjection;

namespace Msdi.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
