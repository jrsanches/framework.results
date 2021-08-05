using Framework.Results.Exceptions;
using Framework.Results.Models;
using NSubstitute;
using System;
using Xunit;

namespace Framework.Results.Tests.Source.Models
{
    public class ResultTests
    {
        [Fact]
        public void Should_Return_Success_Correctly()
        {
            //act
            var result = Result.Success();

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Null(result.Message);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Success_WithMessage_Correctly()
        {
            //arrange
            var message = "any message";

            //act
            var result = Result.Success(message);

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_Correctly()
        {
            //act
            var result = Result.Fail();

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Null(result.Message);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithMessage_Correctly()
        {
            //arrange
            var message = "any message";

            //act
            var result = Result.Fail(message);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");

            //act
            var result = Result.Fail(exception);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(exception.Message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_AndMessage_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");
            var message = "any message";

            //act
            var result = Result.Fail(exception, message);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_ThrowsException_OnFailed_Correctly()
        {
            //arrange
            var message = "any message";

            //act
            var exception = Assert.Throws<ResultException>(() =>
            {
                Result.Fail(message).OnFailedThrowsException();
            });

            var result = exception.Result;

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Null(result.Exception);
            Assert.Equal(message, result.Message);

            Assert.Equal(message, exception.Message);
            Assert.IsType<ResultException>(exception);
        }

        [Fact]
        public void Should_Invoke_CallbackAction_OnFailed_Correctly()
        {
            //arrange
            var anyAbstraction = Substitute.For<IAnyAbstraction>();
            var message = "any message";

            //act
            var result = Result.Fail(message)
                .OnFailed(e => anyAbstraction.DoSomething())
                .OnSuccess(e => anyAbstraction.DoSomethingElse());

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Null(result.Exception);

            anyAbstraction.Received(1).DoSomething();
            anyAbstraction.DidNotReceive().DoSomethingElse();
        }

        [Fact]
        public void Should_Invoke_CallbackAction_OnSuccess_Correctly()
        {
            //arrange
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result.Success()
                .OnSuccess(e => anyAbstraction.DoSomething())
                .OnFailed(e => anyAbstraction.DoSomethingElse());

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Null(result.Message);
            Assert.Null(result.Exception);

            anyAbstraction.Received(1).DoSomething();
            anyAbstraction.DidNotReceive().DoSomethingElse();
        }
    }
}
