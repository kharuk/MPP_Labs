using System;
using Microsoft.Owin.Hosting;
using DAL;

namespace REST
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8282"))
            {
                Console.WriteLine("Serever started...");
                Console.ReadKey();
            }
        }
    }
}
