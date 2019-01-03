using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injector
{
    public interface IInjectorService
    {

        /// <summary>
        /// Registers pair of types
        /// </summary>
        /// <typeparam name="TContract">Interface/Base class</typeparam>
        /// <typeparam name="TImpl">Implementation class</typeparam>
        void Register<TContract, TImpl>(TimeToLive timeToLive = TimeToLive.InstancePerDependency)
            where TContract : class
            where TImpl : TContract;

        /// <summary>
        /// Returns created object with all dependencies
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>created object with all dependencies</returns>
        IList<T> Resolve<T>();

    }
}
