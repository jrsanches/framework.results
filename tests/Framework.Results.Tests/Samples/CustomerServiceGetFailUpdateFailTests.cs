using Framework.Results.Exceptions;
using Framework.Results.Samples;
using Framework.Results.Samples.Models;
using System.Threading.Tasks;
using Xunit;

namespace Framework.Results.Tests.Samples
{
    public class CustomerServiceGetFailUpdateFailTests
    {
        private readonly CustomerServiceGetFailUpdateFail _sut;

        public CustomerServiceGetFailUpdateFailTests()
        {
            _sut = new CustomerServiceGetFailUpdateFail();
        }

        [Fact]
        public async Task Should_ReturnFalse_When_ProcessCustomerCheckingResult()
        {
            //act
            var result = await _sut.ProcessCustomerCheckingResult();

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_ProcessCustomerThrowingException()
        {
            //act
            var exception = await Assert.ThrowsAsync<ResultDataException<Customer>>(async () =>
            {
                await _sut.ProcessCustomerThrowingException();
            });

            //assert
            Assert.IsType<ResultDataException<Customer>>(exception);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_ProcessCustomerReturningResultStatus()
        {
            //act
            var result = await _sut.ProcessCustomerReturningResultStatus();

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_ProcessCustomerReturningCallbackResultStatus()
        {
            //act
            var result = await _sut.ProcessCustomerReturningCallbackResultStatus();

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_Return_ResultFailed_When_ProcessCustomerReturningResult()
        {
            //act
            var result = await _sut.ProcessCustomerReturningResult();

            //assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task Should_Return_ResultFailed_When_ProcessCustomerReturningCallbackResult()
        {
            //act
            var result = await _sut.ProcessCustomerReturningCallbackResult();

            //assert
            Assert.False(result.Succeeded);
        }
    }
}
