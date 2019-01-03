using DB;
using Model;
using Observer;
using System;

namespace DAL
{
    public class Create : IObserver
    {
        internal Create()
        {
            ConcretObservable.instanse.AddObserver(this);
        }

        public T Update<T>(Message message)
        where T : class
        {
            if (message.Request != RequestType.Post)
                return null;

            Result<Book> result = new Result<Book>();

            if (!(result is T))
                throw new ArgumentException();

            return EFCreate(message, result) as T;
        }

        private Result<Book> EFCreate(Message message, Result<Book> result)
        {
            using (var context = new BookDBContext())
            {
                if (!(message.Data is Book data))
                    throw new ArgumentNullException();

                result.Value = context.Books.Add(data);
                if (result.Value == null)
                {
                    result.isOk = false;
                }
                else
                {
                    context.SaveChanges();
                    result.isOk = true;
                }
            }

            return result;
        }
    }
}
