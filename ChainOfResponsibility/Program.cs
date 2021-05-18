using System;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var requestParser = new RequestParser();
            var newRequest = new Request()
            {
                Content = "ahashasd",
                ContentType = "http",
            };

            requestParser.Parse(newRequest);
        }
    }
}
