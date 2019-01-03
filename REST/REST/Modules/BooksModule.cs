using Nancy;
using Nancy.ModelBinding;
using Observer;
using DAL;
using Model;
using System.Collections.Generic;
using ValidationLib.Services;
using ValidationLib.Services.Helpers;
using Injector;
using System.Linq;
using Logger.Interfaces;
using Logger;

namespace REST
{
    public class BooksModule : NancyModule
    {
        private static readonly bool isInit = InitAll();
        private static bool InitAll()
        {
            IInjectorService injector = InjectorService.instance;
            injector.Register<IValidationService, ValidationService>();
            injector.Register<ILogger, ValidationLogger>();

            Init.Initialize();

            return true;
        }
        public BooksModule(InjectorService injectorService) : base("/api/")
        {
            Get["books/{id}"] = parameters => GetById(parameters.id);

            Post["books"] = index => Create();

            Put["books/{id}"] = parameters => Update(parameters.id);

            Get["books"] = _ => GetAll();

            Delete["books/{id}"] = parameters => Remove(parameters.id);
        }

        private Response GetById(int bookId)
        {
            Message message = new Message(RequestType.Get, bookId);
            Result<Book> result = ConcretObservable.instanse.NotifyObserver<Result<Book>>(message);

            if (!result.isOk)
            {
                return Response.AsJson(string.Format(Resource.NotFound, bookId), HttpStatusCode.NotFound);
            }
            return Response.AsJson(result.Value, HttpStatusCode.OK);
        }

        private Response GetAll()
        {
            Message message = new Message(RequestType.GetAll, null);
            Result<IEnumerable<Book>> result = ConcretObservable.instanse.NotifyObserver<Result<IEnumerable<Book>>>(message);

            if (!result.isOk)
            {
                return Response.AsJson(Resource.NotFoundAll, HttpStatusCode.NotFound);
            }
            return Response.AsJson(result.Value, HttpStatusCode.OK);
        }

        private Response Create()
        {
            Book book = this.Bind<Book>();
            Message message = new Message(RequestType.Post, book);
            ValidationResult valResult = InjectorService.instance.Resolve<IValidationService>().First().Validate(book);
            if (valResult.IsOK)
            {
                Result<Book> result = ConcretObservable.instanse.NotifyObserver<Result<Book>>(message);

                if (!result.isOk)
                {
                    return Response.AsJson(Resource.NotFoundWithCreating, HttpStatusCode.NotFound);
                }

                return Response.AsJson(result.Value, HttpStatusCode.Created)
                    .WithHeader("location", string.Format(Resource.Location, result.Value.Id));
            }

            return Response.AsJson(valResult.ValidationErrors, HttpStatusCode.BadRequest);
        }

        private Response Remove(int bookId)
        {
            Message message = new Message(RequestType.Delete, bookId);
            Result<Book> result = ConcretObservable.instanse.NotifyObserver<Result<Book>>(message);

            if (!result.isOk)
            {
                return Response.AsJson(string.Format(Resource.NotFound, result.Value.Id), HttpStatusCode.NotFound);
            }

            return Response.AsJson(string.Format(Resource.DeleteMessage, result.Value.Id), HttpStatusCode.OK);
        }

        private Response Update(int bookId)
        {
            Book book = this.Bind<Book>();
            book.Id = bookId;
            Message message = new Message(RequestType.Put, book);
            ValidationResult valResult = InjectorService.instance.Resolve<IValidationService>().First().Validate(book);
            if (valResult.IsOK)
            {
                Result<Book> result = ConcretObservable.instanse.NotifyObserver<Result<Book>>(message);

                if (!result.isOk)
                {
                    return Response.AsJson(string.Format(Resource.NotFound, result.Value.Id), HttpStatusCode.NotFound);
                }

                return Response.AsJson(result.Value, HttpStatusCode.OK);
            }

            return Response.AsJson(valResult.ValidationErrors, HttpStatusCode.BadRequest);
        }
    }
}
