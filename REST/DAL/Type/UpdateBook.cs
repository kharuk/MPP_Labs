using Model;
using System;
using Observer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;

namespace DAL
{
    public class UpdateBook : IObserver
    {
        internal UpdateBook()
        {
            ConcretObservable.instanse.AddObserver(this);
        }

        public T Update<T>(Message message)
        where T : class
        {
            if (message.Request != RequestType.Put)
                return null;

            Result<Book> result = new Result<Book>();

            if (!(result is T))
                throw new ArgumentException();

            return EFUpdate(message, result) as T;
        }

        private Result<Book> EFUpdate(Message message, Result<Book> result)
        {
            using (var context = new BookDBContext())
            {
                if (!(message.Data is Book data)) throw new ArgumentNullException();

                Book updatedBook = context.Books.Where(book => book.Id == data.Id).FirstOrDefault();
                
                if (updatedBook == null)
                {
                    result.isOk = false;
                    result.Value = new Book { Id = data.Id };
                }
                else
                {
                    context.Entry(updatedBook).CurrentValues.SetValues(data);
                    context.SaveChanges();
                    result.Value = updatedBook;
                    result.isOk = true;
                }
            }

            return result;
        }
    }
}
