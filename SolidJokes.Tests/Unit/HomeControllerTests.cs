using System.Collections.Generic;
using System.Web.Mvc;
using SolidJokes.Core.Models;
using SolidJokes.Tests.Fakes;
using SolidJokes.Web.Controllers;
using Xunit;

namespace SolidJokes.Tests.Unit {
    public class HomeControllerTests {
        [Fact]
        public void HomeController_WhenPassedInFakeJokeViewerReturning2Jokes_ShouldPassBackListOf2Jokes() {
            var viewer = new FakeJokeViewer();
            var voter = new FakeJokeVoter();
            var controller = new HomeController(viewer, voter);

            var result = controller.Index(sortOrder:null, message:null) as ViewResult;

            var list = (List<Joke>)result.Model;
            Assert.Equal(2, list.Count);
        }
    }
}