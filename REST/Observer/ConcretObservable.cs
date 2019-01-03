using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    public class ConcretObservable : IObservable
    {
        public static ConcretObservable instanse = new ConcretObservable();
        private List<IObserver> observers;
        private bool _isDisposed;

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool flag)
        {
            if (_isDisposed)
                return;
            if (flag)
                GC.SuppressFinalize(this);
            observers = null;
            this._isDisposed = true;
        }

        ~ConcretObservable()
        {
            this.Dispose(false);
        }

        public ConcretObservable()
        {
            observers = new List<IObserver>();
        }

        public void AddObserver(IObserver obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Observer must refer to an object!");
            observers.Add(obj);
        }

        public void RemoveObserver(IObserver obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Observer must refer to an object!");
            observers.Remove(obj);
        }

        public T NotifyObserver<T>(Message message) where T : class
        {
            List<T> list = new List<T>();
            foreach (IObserver observer in observers)
            {
                list.Add(observer.Update<T>(message));
            }
            return list.Where(elem => elem != null).Single();
        }
    }
}
