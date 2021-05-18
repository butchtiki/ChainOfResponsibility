using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public class HttpRequestHandler : BaseRequestHandler
    {
        public override void HandleRequest(Request request)
        {
            if (request.ContentType == "http")
            {
                Console.WriteLine("The Http Request has been deserialized");
            }
            else if (this.sucessor != null)
            {
                this.sucessor.HandleRequest(request);
            }
            else
            {
                
                Console.WriteLine("Invalid content type");
                throw new Exception();
            }
        }
    }
}
