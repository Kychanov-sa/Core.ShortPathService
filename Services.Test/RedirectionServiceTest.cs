using GlacialBytes.Core.ShortPathService.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Test.TestContexts;
using System;

namespace Services.Test
{
  [TestClass]
  public class RedirectionServiceTest : BaseServicesTest
  {
    private readonly GivenRedirectionServiceCreated _textContext = new ();

    [TestInitialize]
    public void InitializeTest()
    {
      _textContext.Initialize(DatabaseConnectionString);
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _textContext.Cleanup();
    }

    [TestMethod]
    public void GetRedirectUrl()
    {
      var redirectionUrl = _textContext.RedirectionService.GetRedirectUrl(ValidRouteId);
      Assert.IsNotNull(redirectionUrl);
      Assert.AreEqual(TestRouteUrl, redirectionUrl.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRedirectUrl_WhenRouteIdIsInvalid()
    {
      _ = _textContext.RedirectionService.GetRedirectUrl(InvalidRouteId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteNotFoundException))]
    public void GetRedirectUrl_WhenRouteIsNotExist()
    {
      _ = _textContext.RedirectionService.GetRedirectUrl(NotExistsRouteId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteExpiredException))]
    public void GetRedirectUrl_WhenRouteIsExpired()
    {
      _ = _textContext.RedirectionService.GetRedirectUrl(ExpiredRouteId);
    }
  }
}
