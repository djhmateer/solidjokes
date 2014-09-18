using Core.Models;
using SolidJokes.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace SolidJokes.Tests {
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