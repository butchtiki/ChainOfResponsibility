using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public class DefaultRequestHandler : BaseRequestHandler
    {
        public override void HandleRequest(Request request)
        {
            if (request.Content != null && request.Content.Trim().Length != 0)
            {
                if (this.sucessor != null)
                {
                    this.sucessor.HandleRequest(request);
                }
                else
                {
                    throw new Exception("Request Handled");
                }
            }
            else
            {
                Console.WriteLine("Invalid Content");
                throw new Exception("Invalid Content");
            }

        }
    }
}
