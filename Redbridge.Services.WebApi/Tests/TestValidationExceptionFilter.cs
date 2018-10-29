using Moq;
using NUnit.Framework;
using Redbridge.Diagnostics;
using Redbridge.Services.WebApi.Filters;
using System;
using System.Web;
using System.Web.Http.Filters;

namespace Redbridge.Services.WebApi.Tests
{
    [TestFixture()]
    public class TestValidationExceptionFilter
    {
        [Test()]
        public void FilterValidationExceptionSingleType()
        {
            var logger = new Mock<ILogger>();
            var filter = new ValidationExceptionFilter(logger.Object);
            filter.OnException(new HttpActionExecutedContext() { });
        }
    }
}
