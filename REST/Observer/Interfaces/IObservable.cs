using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    interface IObservable
    {
        /// <summary>
        /// Ads observer to observers list
        /// </summary>
        /// <param name="obj">Observer that should be added</param>
        void AddObserver(IObserver obj);

        /// <summary>
        /// Remouves observer from observers list
        /// </summary>
        /// <param name="obj">Observer that should be remouved</param>
        void RemoveObserver(IObserver obj);

        /// <summary>
        /// Notifies observers about some changes
        /// </summary>
        /// <param name="message">Message that shows what happend</param>
        T NotifyObserver<T>(Message message) where T : class;
    }
}
