using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    public interface IObserver
    {
        /// <summary>
        /// Perfoms some action
        /// </summary>
        /// <param name="message">Message resieved from observable</param>
        T Update<T>(Message type) where T : class;
    }
}
