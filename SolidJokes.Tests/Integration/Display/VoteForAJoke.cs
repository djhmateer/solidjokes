using System.Linq;
using SolidJokes.Core.DB;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.Display {
    [Trait("Homepage", "User votes for a Joke")]
    public class VoteForAJoke : IntegrationTestBaseWithData {
        readonly JokeVoter jokeVoter;
        public VoteForAJoke() {
            jokeVoter = new JokeVoter(new Session());
        }

        [Fact(DisplayName = "Rating of Joke is incremented by 1")]
        public void RatingIsIncrementedBy1() {
            var session = new Session();
            var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");
            Assert.Equal(10, bananaJoke.Rating);
            var result = jokeVoter.AddVote(bananaJoke.ID);
            Assert.Equal(11, result.Joke.Rating);
        }

        [Fact(DisplayName = "A messsage is provided to the User")]
        public void AMessageIsProvidedForUser() {
            var session = new Session();
            var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");
            var result = jokeVoter.AddVote(bananaJoke.ID);
            Assert.Equal("Thank you for voting!", result.Message);
            Assert.Equal(true, result.VoteSucceeded);
        }

        //[Fact(DisplayName = "Default of ratingDescending sort order is remembered")]
        //public void SortOrder() {
        //    // Get JokeID of last one entered
        //    var session = new Session();
        //    int storyID = session.Stories.FirstOrDefault(st => st.Rating == 5).ID;

        //    var controller = new HomeController();
        //    // Rating entered as 10,2,5.. so this sort order is 10,5,2
        //    var result = controller.Index(sortOrder: "ratingDescending") as ViewResult;

        //    // Vote for the last story
        //    var redirectResult = (RedirectToRouteResult)controller.Vote(storyID);

        //    // Want to assert the RedirectToAction has worked
        //    Assert.Equal(redirectResult.RouteValues["sortOrder"], "ratingDescending");
        //}

        //[Fact(DisplayName = "Sorting by date with newest first is remembered")]
        //public void SortOrderDateDescending() {
        //    // Get JokeID of last one entered
        //    var session = new Session();
        //    int storyID = session.Stories.FirstOrDefault(st => st.Rating == 5).ID;

        //    var controller = new HomeController();
        //    var result = controller.Index(sortOrder: "dateCreatedDescending") as ViewResult;

        //    // Vote for the last story
        //    var redirectResult = (RedirectToRouteResult)controller.Vote(storyID, sortOrder: "dateCreatedDescending");

        //    // Want to assert the RedirectToAction has worked
        //    Assert.Equal(redirectResult.RouteValues["sortOrder"], "dateCreatedDescending");
        //}
    }

}
