using System.Collections.Generic;
using System.Web.Mvc;
using Core.Models;
using SolidJokes.Controllers;
using SolidJokes.Tests.Fakes;
using SolidJokes.Web.Controllers;
using Xunit;

namespace SolidJokes.Tests.Unit {
    public class HomeControllerTests {
        [Fact]
        public void HomeController_WhenPassedInFakeJokeViewerReturning2Jokes_ShouldPassBackListOf2Jokes() {
            var viewer = new FakeJokeViewer();
            var controller = new HomeController(viewer);

            var result = controller.Index() as ViewResult;

            var list = (List<Joke>)result.Model;
            Assert.Equal(2, list.Count);
        }
    }
}