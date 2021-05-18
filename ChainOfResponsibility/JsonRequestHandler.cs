using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace ChainOfResponsibility
{
    public class JsonRequestHandler : BaseRequestHandler
    {
        public override void HandleRequest(Request request)
        {
            if(request.ContentType == "json")
            {
                Console.WriteLine("The JSON Request has been deserialized");
            }
            else if (this.sucessor != null)
            {
                this.sucessor.HandleRequest(request);
            }
            else
            {
                //Debug.WriteLine("Exception") ;
                throw new Exception();
            }
            
        }


    }
}
