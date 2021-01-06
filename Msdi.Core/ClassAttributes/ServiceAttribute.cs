using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Msdi.Core.Enumerations.ServiceLifetime;

namespace Msdi.Core.ClassAttributes
{
    /// <summary>
    /// ServiceLifeTimeAttribute- Used to determine how a service should be injected.
    /// Can be used only for classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class Service : Attribute
    {

        /// <summary>
        /// Gets the lifetime.
        /// </summary>
        /// <value>
        /// The lifetime.
        /// </value>
        public Lifetime Lifetime { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lifetime">Lifetime attribute</param>
        public Service(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// Gets the Lifetime of current attribute
        /// </summary>
        /// <returns></returns>
        public Lifetime GetLifetime()
        {
            return Lifetime;
        }

    }
}
