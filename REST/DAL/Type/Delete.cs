using DB;
using Model;
using Observer;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class Delete : IObserver
    {
        internal Delete()
        {
            ConcretObservable.instanse.AddObserver(this);
        }

        public T Update<T>(Message message)
        where T : class
        {
            if (message.Request != RequestType.Delete)
                return null;

            Result<Book> result = new Result<Book>();

            if (!(result is T))
                throw new ArgumentException();

            return EFDelete(message, result) as T;
        }

        private Result<Book> EFDelete(Message message, Result<Book> result)
        {
            using (var context = new BookDBContext())
            {
                if (!(message.Data is int data)) throw new ArgumentNullException();

                Book deletedBook = context.Books.Where(book => book.Id == data).FirstOrDefault();

                if (deletedBook == null)
                {
                    result.isOk = false;
                    result.Value = new Book { Id = data };
                }
                else
                {
                    result.Value = context.Books.Remove(deletedBook);
                    context.SaveChanges();
                    result.isOk = true;
                }
            }

            return result;
        }
    }
}
