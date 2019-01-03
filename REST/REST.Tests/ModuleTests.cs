using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Nancy;
using Nancy.Testing;
using System.Text.RegularExpressions;

namespace REST.Tests
{
    [TestClass]
    public class ModuleTests
    {


        private static ConfigurableBootstrapper bootstrapper = new ConfigurableBootstrapper(with => {
            with.Module<BooksModule>();
        });
        private Browser browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

        [TestMethod]
        public void OK_On_GetId()
        {
            var result = browser.Get("/api/books/6", with => {
                with.HttpRequest();
            });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void OK_On_GetId_NotFound()
        {
            var result = browser.Get("/api/books/1111", with => {
                with.HttpRequest();
            });
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void OK_On_Get()
        {
            var result = browser.Get("/api/books", with => {
                with.HttpRequest();
            });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Created_On_Create()
        {

            var result = browser.Post("/api/books", with =>
            {
                with.JsonBody<Book>(new Book { Name = "title5", Price = 10, Author = "Luke", CountOfPages = 15});
            });

            Assert.IsTrue(Regex.IsMatch(result.Headers["location"], @"/api/books/(\d+)"));
            Assert.IsTrue(result.Headers["location"].Contains("/api/books/"));
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void BadRequest_On_Create()
        {
            var result = browser.Post("/api/books", with =>
            {
                with.JsonBody<Book>(new Book { Name = "ti", Price = 10, Author = "Luke", CountOfPages = 15 });
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void OK_On_Update()
        {
            var result = browser.Put("/api/books/6", with =>
            {
                with.JsonBody<Book>(new Book { Name = "title5", Price = 10, Author = "Luke", CountOfPages = 15 });
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void BadRequest_On_Update()
        {
            var result = browser.Put("/api/books/1", with =>
            {
                with.JsonBody<Book>(new Book { Name = "title5", Price = -5, Author = "Luke", CountOfPages = 15 });
            });

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
