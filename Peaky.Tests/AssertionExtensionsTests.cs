using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Peaky.Tests
{
    public class AssertionExtensionsTests
    {
        [Fact]
        public async Task When_ShouldSucceed_is_passed_a_failed_response_it_throws()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            Action assert = () => response.ShouldSucceed();

            assert.ShouldThrow<AssertionFailedException>();
        }

        [Fact]
        public async Task When_ShouldFailWith_is_passed_a_successful_response_it_throws()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            Action assert = () => response.ShouldFailWith(HttpStatusCode.Forbidden);

            assert.ShouldThrow<AssertionFailedException>();
        }

        [Fact]
        public async Task When_ShouldSucceed_is_passed_a_successful_response_it_doesnt_throw()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);

            Action assert = () => response.ShouldSucceed();

            assert.ShouldNotThrow();
        }

        [Fact]
        public async Task When_ShouldFailWith_is_passed_a_failed_response_it_doesnt_throw()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            Action assert = () => response.ShouldFailWith(HttpStatusCode.BadRequest);

            assert.ShouldNotThrow();
        }

        [Fact]
        public async Task When_ShouldSucceedAsync_is_passed_a_failed_response_it_throws()
        {
            var response = Task.Run(() => new HttpResponseMessage(HttpStatusCode.BadRequest));

            Func<Task> assert = () => response.ShouldSucceedAsync();

            assert.ShouldThrow<AssertionFailedException>();
        }

        [Fact]
        public async Task When_ShouldFailWithAsync_is_passed_a_successful_response_it_throws()
        {
            var response = Task.Run(() => new HttpResponseMessage(HttpStatusCode.OK));

            Func<Task> assert = () => response.ShouldFailWithAsync(HttpStatusCode.Forbidden);

            assert.ShouldThrow<AssertionFailedException>();
        }

        [Fact]
        public async Task When_ShouldSucceedAsync_is_passed_a_successful_response_it_doesnt_throw()
        {
            var response = Task.Run(() => new HttpResponseMessage(HttpStatusCode.Accepted));

            Action assert = () =>
            {
                var x = response.ShouldSucceedAsync().Result;
            };

            assert.ShouldNotThrow();
        }

        [Fact]
        public async Task When_ShouldFailWithAsync_is_passed_a_failed_response_it_doesnt_throw()
        {
            var response = Task.Run(() => new HttpResponseMessage(HttpStatusCode.BadRequest));

            Action assert = () =>
            {
                var x = response.ShouldFailWithAsync(HttpStatusCode.BadRequest).Result;
            };

            assert.ShouldNotThrow();
        }
    }
}
