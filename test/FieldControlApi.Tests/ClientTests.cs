﻿using FieldControlApi.Exceptions;
using FieldControlApi.Lib;
using FieldControlApi.Requests;
using FieldControlApi.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace FieldControlApi.Tests
{
    /*
        [✓] Authenticate should set _authenticationToken in case of success
        [✓] Authenticate should throws AuthenticationException in case of failure

        [✓] SetAuthenticationToken should set request.Token when client is authenticated (contains authentication token)
        [✓] SetAuthenticationToken should throws ClientNotAuthenticatedException when client is not authenticated (does not contains authentication token)

        [✓] Execute should call _httpRequester.ExecuteRequest
    */

    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void Authenticate_InCaseOfSuccess_ShouldSetAuthenticationToken()
        {
            var httpRequesterMock = new Mock<IHttpRequester>();
            httpRequesterMock.Setup(c => c.ExecuteRequest(It.IsAny<Request>()))
                             .Returns(new HttpRequesterResponse() {
                                  HttpStatusCode = 200,
                                  ResponseContent = "{\"success\":true,\"token\":\"abc\"}"
                              });

            var client = new Client(httpRequesterMock.Object);
            client.Authenticate("email", "password");

            Assert.AreEqual("abc", client.AuthenticationToken);
        }

        [Test]
        public void Authenticate_InCaseOfFailure_ShouldThrowsAuthenticationException()
        {
            var httpRequesterMock = new Mock<IHttpRequester>();
            httpRequesterMock.Setup(c => c.ExecuteRequest(It.IsAny<Request>()))
                             .Returns(new HttpRequesterResponse()
                             {
                                 HttpStatusCode = 200,
                                 ResponseContent = "{\"success\":false,\"token\":null}"
                             });

            var client = new Client(httpRequesterMock.Object);

            Assert.Throws<AuthenticationException>(() => client.Authenticate("email", "password"));
        }

        [Test]
        public void SetAuthenticationToken_WhenClientContainsAuthenticationToken_RequestTokenShouldBeSet()
        {
            var client = new ClientProxy()
            {
                AuthenticationToken = "abc"
            };
            
            var request = new DummyRequest(new DummyResource());
            client.SetAuthenticationToken(request);

            Assert.AreEqual("abc", request.Token);
        }

        [Test]
        public void SetAuthenticationToken_WhenClientDoesNotContainsAuthenticationToken_ShouldThrowsClientNotAuthenticatedException()
        {
            var client = new ClientProxy();

            Assert.Throws<ClientNotAuthenticatedException>(() => client.SetAuthenticationToken(new DummyRequest(new DummyResource())));
        }

        [Test]
        public void Execute_ShouldCallExecuteRequestOnHttpRequesterOnce()
        {
            var httpRequesterMock = new Mock<IHttpRequester>();
            httpRequesterMock.Setup(c => c.ExecuteRequest(It.IsAny<Request>()))
                             .Returns(new HttpRequesterResponse()
                             {
                                 HttpStatusCode = 200,
                                 ResponseContent = "{\"id\":1}"
                             });

            var client = new Client(httpRequesterMock.Object);
            client.AuthenticationToken = "abc";
            client.Execute(new DummyRequest(new DummyResource()));

            httpRequesterMock.Verify(c => c.ExecuteRequest(It.IsAny<Request>()), Times.Once);
        }
    }
}
