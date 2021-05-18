using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainOfResponsibility;
using Moq;
using System;

namespace ChainOfResponsibilityTest
{
    [TestClass]
    public class JsonRequestHandlerTest
    {
        JsonRequestHandler jsonRequestHandler;
        Request newRequest;
        Mock<BaseRequestHandler> mockHttpRequestHandler;

        [TestInitialize]
        public void SetUp()
        {
            this.mockHttpRequestHandler = new Mock<BaseRequestHandler>();
            this.jsonRequestHandler = new JsonRequestHandler();
        }

        [TestMethod]
        public void HandleRequest_JsonContentTypeANDSucessorNotNull_ContentDeserializedANDEndChain()
        {
            this.newRequest = new Request()
            {
                ContentType = "json",
                Content = "This is a random json content"
            };
            this.jsonRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);

            jsonRequestHandler.HandleRequest(newRequest);

            this.mockHttpRequestHandler.Verify(mockHttpRequestHandler =>
                mockHttpRequestHandler.HandleRequest(newRequest), Times.Never);

        }

        [TestMethod]
        public void HandleRequest_JsonContentTypeANDSucessorNull_ContentDeserialized()
        {
            newRequest = new Request()
            {
                ContentType = "json",
                Content = "asasas"
            };
            this.jsonRequestHandler.SetSucessor(null);

            try
            {
                this.jsonRequestHandler.HandleRequest(newRequest);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HandleRequest_NotJsonContentTypeANDSucessorNotNull_SucessorHandlesRequest()
        {
            newRequest = new Request()
            {
                ContentType = "http",
                Content = "asasas"
            };
            this.jsonRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);
            this.mockHttpRequestHandler.Setup(a => a.HandleRequest(newRequest));

            this.jsonRequestHandler.HandleRequest(newRequest);

            this.mockHttpRequestHandler.Verify(a => a.HandleRequest(newRequest), Times.Once);
        }

        [TestMethod]
        public void HandleRequest_NotJsonContentTypeANDSucessorNull_InvalidContentType()
        {
            newRequest = new Request()
            {
                ContentType = "http",
                Content = "This is a random content"
            };

            this.mockHttpRequestHandler.Setup(mockHttpRequestHandler => mockHttpRequestHandler.HandleRequest(newRequest));
            this.jsonRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);
            
            try
            {
                jsonRequestHandler.HandleRequest(newRequest);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
            
            
        }
    }
}
