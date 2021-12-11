using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace mode_platonic_api.UnitTests.Common
{
    public static class ExtensionMethods
    {
        public static async Task<T> AssertSuccessful<T>(this Task<ActionResult<T>> actionResult) {
            var result = (await actionResult).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

            return (T)result.Value;
        }


        public static async Task AssertStatusCode<T>(this Task<ActionResult<T>> actionResult, HttpStatusCode statusCode) {
            var result = (await actionResult).Result as StatusCodeResult; 

            Assert.NotNull(result);
            Assert.Equal(statusCode, (HttpStatusCode)result.StatusCode);
        }
    }
}
