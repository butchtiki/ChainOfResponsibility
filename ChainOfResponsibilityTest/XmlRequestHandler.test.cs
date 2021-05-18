using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainOfResponsibility;
using Moq;
using System;

namespace ChainOfResponsibilityTest
{
    [TestClass]
    public class XmlRequestHandlerTest
    {
        XmlRequestHandler xmlRequestHandler;
        Request newRequest;
        Mock<BaseRequestHandler> mockHttpRequestHandler;

        [TestInitialize]
        public void SetUp()
        {
            this.mockHttpRequestHandler = new Mock<BaseRequestHandler>();
            this.xmlRequestHandler = new XmlRequestHandler();
        }

        [TestMethod]
        public void HandleRequest_XmlContentTypeANDSucessorNotNull_ContentDeserializedANDEndChain()
        {
            this.newRequest = new Request()
            {
                ContentType = "xml",
                Content = "This is a random xml content"
            };
            this.xmlRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);

            xmlRequestHandler.HandleRequest(newRequest);

            this.mockHttpRequestHandler.Verify(mockHttpRequestHandler =>
                mockHttpRequestHandler.HandleRequest(newRequest), Times.Never);

        }

        [TestMethod]
        public void HandleRequest_XmlContentTypeANDSucessorNull_ContentDeserialized()
        {
            newRequest = new Request()
            {
                ContentType = "xml",
                Content = "asasas"
            };
            this.xmlRequestHandler.SetSucessor(null);

            try
            {
                this.xmlRequestHandler.HandleRequest(newRequest);
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
            this.xmlRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);
            this.mockHttpRequestHandler.Setup(a => a.HandleRequest(newRequest));


            this.xmlRequestHandler.HandleRequest(newRequest);


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

            this.xmlRequestHandler.SetSucessor(null);

            try
            {
                xmlRequestHandler.HandleRequest(newRequest);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
