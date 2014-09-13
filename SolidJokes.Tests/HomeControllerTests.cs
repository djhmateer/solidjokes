using Core.Models;
using Core.Services;
using SolidJokes.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace SolidJokes.Tests {
    public class HomeControllerTests {
        [Fact]
        public void HomeController_WhenPassedInFakeJokeViewerReturning2Jokes_ShouldPassBackListOf2Jokes() {
            var viewer = new FakeJokeViewerReturning2Jokes();
            var controller = new HomeController(viewer);

            var result = controller.Index() as ViewResult;

            var list = (List<Joke>)result.Model;
            Assert.Equal(2, list.Count);
        }
    }

    public class FakeJokeViewerReturning2Jokes : IJokeViewer {
        public List<Joke> ShowAllJokesHighestRatingFirst() {
            return new List<Joke> { new Joke(), new Joke() };
        }
    }
}