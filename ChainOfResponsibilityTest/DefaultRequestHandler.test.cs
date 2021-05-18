using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using ChainOfResponsibility;

namespace ChainOfResponsibilityTest
{
    [TestClass]
    public class DefaultRequestHandlerTest
    {
        DefaultRequestHandler defaultRequestHandler;
        Request newRequest;
        Mock<BaseRequestHandler> mockHttpRequestHandler;

        [TestInitialize]
        public void SetUp()
        {
            this.mockHttpRequestHandler = new Mock<BaseRequestHandler>();
            this.defaultRequestHandler = new DefaultRequestHandler();
        }
        
        [TestMethod]
        public void HandleRequest_NotNullContentNotWhitespaceContentNotNullSucessor_SucessorHandlesRequest()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = "Random Content"
            };
            this.mockHttpRequestHandler.Setup(mockhttpRequestHandler => mockhttpRequestHandler.HandleRequest(newRequest));
            this.defaultRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                this.mockHttpRequestHandler.Verify(mockhttpRequestHandler => mockhttpRequestHandler.HandleRequest(newRequest), Times.Once);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void HandleRequest_NotNullContentNotWhitespaceContentNullSucessor_RequestHandled()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = "RandomContent"
            };
            
            this.defaultRequestHandler.SetSucessor(null);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Request Handled");
            }

        }

        [TestMethod]
        public void HandleRequest_NotNullContentWhitespaceContentNotNullSucessor_InvalidContent()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = "   "
            };
            this.mockHttpRequestHandler.Setup(mockhttpRequestHandler => mockhttpRequestHandler.HandleRequest(newRequest));
            this.defaultRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                this.mockHttpRequestHandler.Verify(mockhttpRequestHandler => mockhttpRequestHandler.HandleRequest(newRequest), Times.Once);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Invalid Content");
            }

        }

        [TestMethod]
        public void HandleRequest_NotNullContentWhitespaceContentNullSucessor_InvalidContent()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = "   "
            };

            this.defaultRequestHandler.SetSucessor(null);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Invalid Content");
            }

        }


        [TestMethod]
        public void HandleRequest_NullContentNotNullSucessor_InvalidContent()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = null
            };
            this.mockHttpRequestHandler.Setup(mockHttpRequestHandler => mockHttpRequestHandler.HandleRequest(this.newRequest));
            this.defaultRequestHandler.SetSucessor(this.mockHttpRequestHandler.Object);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                this.mockHttpRequestHandler.Verify(mockHttpRequestHandler => mockHttpRequestHandler.HandleRequest(this.newRequest), Times.Never);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Invalid Content");
            }

        }

        [TestMethod]
        public void HandleRequest_NullContentNullSucessor_InvalidContent()
        {
            this.newRequest = new Request()
            {
                ContentType = "http",
                Content = null
            };
            this.defaultRequestHandler.SetSucessor(null);

            try
            {
                this.defaultRequestHandler.HandleRequest(newRequest);
                
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Invalid Content");
            }

        }


    }
}
