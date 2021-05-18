using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public class RequestParser
    {
        public void Parse(Request request)
        {
            var xmlHandler = new XmlRequestHandler();
            var jsonHandler = new JsonRequestHandler();
            var httpHandler = new HttpRequestHandler();
            var defaultHandler = new DefaultRequestHandler();
            
            defaultHandler.SetSucessor(httpHandler);
            httpHandler.SetSucessor(xmlHandler);
            xmlHandler.SetSucessor(jsonHandler);
            
            defaultHandler.HandleRequest(request);

            throw new Exception("Request handled");
            
        }
    }
}
