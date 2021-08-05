using Framework.Results.Exceptions;
using Framework.Results.Models;
using NSubstitute;
using System;
using Xunit;

namespace Framework.Results.Tests.Source.Models
{
    public class DataResultTests
    {
        [Fact]
        public void Should_Return_Success_Correctly()
        {
            //act
            var result = Result<IAnyAbstraction>.Success();

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Null(result.Message);
            Assert.Null(result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Success_WithData_Correctly()
        {
            //arrange
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result<IAnyAbstraction>.Success(anyAbstraction);

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Null(result.Message);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Success_WithData_AndMessage_Correctly()
        {
            //arrange
            var anyAbstraction = Substitute.For<IAnyAbstraction>();
            var message = "any message";

            //act
            var result = Result<IAnyAbstraction>.Success(anyAbstraction, message);

            //assert
            Assert.True(result.Succeeded);
            Assert.False(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_Correctly()
        {
            //act
            var result = Result<IAnyAbstraction>.Fail();

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Null(result.Message);
            Assert.Null(result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithData_Correctly()
        {
            //arrange
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result<IAnyAbstraction>.Fail(anyAbstraction);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Null(result.Message);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithMessage_Correctly()
        {
            //arrange
            var message = "any message";

            //act
            var result = Result<IAnyAbstraction>.Fail(message);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Null(result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithMessage_AndData_Correctly()
        {
            //arrange
            var message = "any message";
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result<IAnyAbstraction>.Fail(message, anyAbstraction);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.Null(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");

            //act
            var result = Result<IAnyAbstraction>.Fail(exception);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(exception.Message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.Null(result.Data);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_AndData_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result<IAnyAbstraction>.Fail(exception, anyAbstraction);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(exception.Message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_AndMessage_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");
            var message = "any message";

            //act
            var result = Result<IAnyAbstraction>.Fail(exception, message);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.Null(result.Data);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_Return_Fail_WithException_AndMessage_AndData_Correctly()
        {
            //arrange
            var exception = new NotImplementedException("exception message");
            var message = "any message";
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var result = Result<IAnyAbstraction>.Fail(exception, message, anyAbstraction);

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Equal(message, result.Message);
            Assert.Equal(exception, result.Exception);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.IsType<NotImplementedException>(result.Exception);
        }

        [Fact]
        public void Should_ThrowsDataException_OnFailed_Correctly()
        {
            //arrange
            var message = "any message";
            var anyAbstraction = Substitute.For<IAnyAbstraction>();

            //act
            var exception = Assert.Throws<ResultDataException<IAnyAbstraction>>(() =>
            {
                Result<IAnyAbstraction>.Fail(message, anyAbstraction)
                    .OnFailedThrowsDataException();
            });

            var result = exception.Result;

            //assert
            Assert.False(result.Succeeded);
            Assert.True(result.Failed);
            Assert.Null(result.Exception);
            Assert.Equal(message, result.Message);
            Assert.Equal(anyAbstraction, result.Data);
            Assert.Equal(message, exception.Message);
            Assert.IsType<ResultDataException<IAnyAbstraction>>(exception);
        }

        [Fact]
        public void Should_ThrowsException_OnFailed_Correctly()
        {
            //arrange
            var message = "any message";

            //act
            var exception = Assert.Throws<ResultException>(() =>
            {
                Result<IAnyAbstraction>.Fail(message).OnFailedThrowsException();
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
            var result = Result<IAnyAbstraction>.Fail(message)
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
            var result = Result<IAnyAbstraction>.Success()
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
