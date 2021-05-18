using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public class Request
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}
