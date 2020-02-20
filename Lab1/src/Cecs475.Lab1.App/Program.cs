using System;
using Cecs475.Lab1.Model;

namespace Cecs475.Lab1.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Need to add the other project to the dependencies of the App project before using
            Class1 c = new Class1();
            Cereal x = new Cereal();
        }
    }
}
