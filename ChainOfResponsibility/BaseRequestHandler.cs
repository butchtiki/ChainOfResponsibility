using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public abstract  class BaseRequestHandler
    {
        protected BaseRequestHandler sucessor;

        public void SetSucessor(BaseRequestHandler sucessor)
        {
            if (sucessor != null)
            {
                this.sucessor = sucessor;
            }
        }

        public abstract void HandleRequest(Request request);
        
    }
}
