using System.Linq;
using System.Web.Mvc;
using SolidJokes.Core.DB;
using SolidJokes.Core.Services;
using SolidJokes.Web.Controllers;
using Xunit;

namespace SolidJokes.Tests.Integration.Display {
    [Trait("Homepage", "User votes for a Joke and SortOrder is remembered")]
    public class VoteForAJokeSortOrderRemembered : IntegrationTestBaseWithData {

        [Fact(DisplayName = "Default of ratingDescending sort order is remembered")]
        public void SortOrder() {
            var session = new Session();
            var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");

            var controller = new HomeController(new JokeViewer(session), new JokeVoter(session));

            var redirectResult = (RedirectToRouteResult)controller.Vote(bananaJoke.ID);

            // Want to assert the RedirectToAction has worked
            Assert.Equal(redirectResult.RouteValues["sortOrder"], "ratingDescending");
        }

        [Fact(DisplayName = "Sorting by date with newest first is remembered")]
        public void SortOrderDateDescending() {
            var session = new Session();
            var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");

            var controller = new HomeController(new JokeViewer(session), new JokeVoter(session));

            var redirectResult = (RedirectToRouteResult)controller.Vote(bananaJoke.ID, sortOrder: "dateCreatedDescending");

            Assert.Equal(redirectResult.RouteValues["sortOrder"], "dateCreatedDescending");
        }
    }
}
