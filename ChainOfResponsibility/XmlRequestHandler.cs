using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public class XmlRequestHandler : BaseRequestHandler
    {
        public override void HandleRequest(Request request)
        {
            if (request.ContentType == "xml")
            {
                Console.WriteLine("The XML Request has been deserialized");
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
