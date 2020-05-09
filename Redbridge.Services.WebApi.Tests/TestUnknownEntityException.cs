using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moq;
using NUnit.Framework;
using Redbridge.Data;
using Redbridge.Diagnostics;
using Redbridge.Exceptions;
using Redbridge.Services.WebApi.Filters;
using Redbridge.Threading;
using Redbridge.Web.Messaging;

namespace Redbridge.Services.WebApi.Tests
{
    [TestFixture]
    public class TestUnknownEntityException
    {
        public class UnknownItemException : UnknownEntityException
        {
            public UnknownItemException (string message) : base(message) {}

            public override string EntityType => "My tester";
        }

        [Test]
        public void ProcessUnknownEntityExceptionConvertTo422Response()
        {
            var logger = new Mock<ILogger>();
            var filter = new UnknownEntityExceptionFilter(logger.Object);

            var httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = new HttpRequestMessage(HttpMethod.Get, "orders/api")
                }
            };

            var context = new HttpActionExecutedContext(httpActionContext, new UnknownItemException("Something is not quite right."));
            filter.OnException(context);
            Assert.IsNull(context.Exception);
            Assert.IsNotNull(context.Response);
        }

        [Test]
        public void ProcessUnknownEntityExceptionConvertToValidationException()
        {
            var logger = new Mock<ILogger>();
            var filter = new UnknownEntityExceptionFilter(logger.Object);

            var httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = new HttpRequestMessage(HttpMethod.Get, "orders/api")
                }
            };

            var context = new HttpActionExecutedContext(httpActionContext, new UnknownItemException("Something is not quite right."));
            filter.OnException(context);

            try
            {
                httpActionContext.Response.ThrowResponseException().WaitAndUnwrapException();
            }
            catch (ValidationException ve)
            {
                Assert.AreEqual("Something is not quite right.", ve.Message);
            }
        }
    }
}
