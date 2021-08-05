using Framework.Results.Samples;
using System.Threading.Tasks;
using Xunit;

namespace Framework.Results.Tests.Samples
{
    public class CustomerServiceGetSuccessUpdateSuccessTests
    {
        private readonly CustomerServiceGetSuccessUpdateSuccess _sut;

        public CustomerServiceGetSuccessUpdateSuccessTests()
        {
            _sut = new CustomerServiceGetSuccessUpdateSuccess();
        }

        [Fact]
        public async Task Should_ReturnTrue_When_ProcessCustomerCheckingResult()
        {
            //act
            var result = await _sut.ProcessCustomerCheckingResult();

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_When_ProcessCustomerThrowingException()
        {
            //act
            var result = await _sut.ProcessCustomerThrowingException();

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_When_ProcessCustomerReturningResultStatus()
        {
            //act
            var result = await _sut.ProcessCustomerReturningResultStatus();

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_When_ProcessCustomerReturningCallbackResultStatus()
        {
            //act
            var result = await _sut.ProcessCustomerReturningCallbackResultStatus();

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_Return_ResultSuccess_When_ProcessCustomerReturningResult()
        {
            //act
            var result = await _sut.ProcessCustomerReturningResult();

            //assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task Should_Return_ResultSuccess_When_ProcessCustomerReturningCallbackResult()
        {
            //act
            var result = await _sut.ProcessCustomerReturningCallbackResult();

            //assert
            Assert.True(result.Succeeded);
        }
    }
}
