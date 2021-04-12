using System;
using System.Threading.Tasks;

namespace NetWorkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("打开网址：https://www.bing.com");
            var url = "https://www.bing.com";
            //var url = Console.ReadLine();

            var browser = new DriverCore();
            browser.Driver.Url = url;

            Task.Delay(10000).Wait();

            foreach (var item in browser.Result)
            {
                Console.WriteLine(item);
            }
            browser.Driver.Quit();
        }
    }
}
