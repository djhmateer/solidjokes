using System.Collections.Generic;
using Core.Models;
using Core.Services;
using SolidJokes.Controllers;
using System.Web.Mvc;
using Xunit;

namespace SolidJokes.Tests {
    public class HomeControllerTests {
        [Fact]
        public void Index() {
            // Arrange
            var viewer = new FakeJokeViewer();
            var controller = new HomeController(viewer);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        //[Fact]
        //public void About() {
        //    var controller = new HomeController();

        //    var result = controller.About() as ViewResult;

        //    Assert.Equal("Your application description page.", result.ViewBag.Message);
        //}

        //[Fact]
        //public void Contact() {
        //    var controller = new HomeController();

        //    var result = controller.Contact() as ViewResult;

        //    Assert.NotNull(result);
        //}
    }

    public class FakeJokeViewer : IJokeViewer {
        public List<Joke> ShowAllJokesHighestRatingFirst() {
            return null;
        }
    }
}