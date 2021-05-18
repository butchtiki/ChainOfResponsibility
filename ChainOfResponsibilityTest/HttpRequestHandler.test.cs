using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainOfResponsibility;
using Moq;
using System;

namespace ChainOfResponsibilityTest
{
    [TestClass]
    public class HttpRequestHandlerTest
    {
        HttpRequestHandler httpRequestHandler;
        Request newRequest;
        Mock<BaseRequestHandler> mockJsonRequestHandler;

        [TestInitialize]
        public void SetUp()
        {
            this.mockJsonRequestHandler = new Mock<BaseRequestHandler>();
            this.httpRequestHandler = new HttpRequestHandler();
        }

        [TestMethod]
        public void HandleRequest_HttpContentTypeANDSucessorNotNull_ContentDeserializedANDEndChain()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = "This is a random http content"
            };
            this.httpRequestHandler.SetSucessor(this.mockJsonRequestHandler.Object);

            httpRequestHandler.HandleRequest(newRequest);

            this.mockJsonRequestHandler.Verify(mockJsonRequestHandler => mockJsonRequestHandler.HandleRequest(newRequest), Times.Never);

        }

        [TestMethod]
        public void HandleRequest_HttpContentTypeANDSucessorNull_ContentDeserialized()
        {
            newRequest = new Request()
            {
                ContentType = "http",
                Content = "asasas"
            };
            this.httpRequestHandler.SetSucessor(null);

            //how to verify that request has been handled? add below
            try
            {
                this.httpRequestHandler.HandleRequest(newRequest);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HandleRequest_NotHttpContentTypeANDSucessorNotNull_SucessorHandlesRequest()
        {
            newRequest = new Request()
            {
                ContentType = "json",
                Content = "asasas"
            };
            this.httpRequestHandler.SetSucessor(this.mockJsonRequestHandler.Object);
            this.mockJsonRequestHandler.Setup(a => a.HandleRequest(newRequest));


            this.httpRequestHandler.HandleRequest(newRequest);


            this.mockJsonRequestHandler.Verify(a => a.HandleRequest(newRequest), Times.Once);
        }

        [TestMethod]
        public void HandleRequest_NotHttpContentTypeANDSucessorNull_InvalidContentType()
        {
            newRequest = new Request()
            {
                ContentType = "json",
                Content = "This is a random content"
            };
            
            this.httpRequestHandler.SetSucessor(null);

            //how to verify if Invalid content type.
            try
            {
                this.httpRequestHandler.HandleRequest(newRequest);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }

        }
    }
}
