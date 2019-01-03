using Observer;
using System;
using DB;
using Model;
using System.Linq;

namespace DAL
{
    public class Get : IObserver
    {
        internal Get()
        {
            ConcretObservable.instanse.AddObserver(this);
        }

        public T Update<T>(Message message)
        where T : class
        {
            if (message.Request != RequestType.Get)
                return null;
            
            Result<Book> result = new Result<Book>();

            if (!(result is T))
                throw new ArgumentException();

            return EFGet(message, result) as T;
        }

        private Result<Book> EFGet(Message message, Result<Book> result)
        {
            using (var context = new BookDBContext())
            {
                int? data = message.Data as int?;

                if (data == null) throw new ArgumentNullException();

                result.Value = context.Books.Where(book => book.Id == data).FirstOrDefault();
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
