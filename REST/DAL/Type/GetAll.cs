using DB;
using Model;
using Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class GetAll : IObserver
    {
        internal GetAll()
        {
            ConcretObservable.instanse.AddObserver(this);
        }

        public T Update<T>(Message message)
        where T : class
        {
            if (message.Request != RequestType.GetAll)
                return null;

            Result<IEnumerable<Book>> result = new Result<IEnumerable<Book>>();

            if (!(result is T))
                throw new ArgumentException();

            return EFGetAll(message, result) as T;
        }

        private Result<IEnumerable<Book>> EFGetAll(Message message, Result<IEnumerable<Book>> result)
        {
            using (var context = new BookDBContext())
            {
                result.Value = context.Books.ToList();

                if (result.Value == null)
                {
                    result.isOk = false;
                }
                else
                {
                    result.isOk = true;
                }
            }

            return result;
        }
    }
}
