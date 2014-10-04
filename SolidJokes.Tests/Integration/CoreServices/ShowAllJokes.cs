using System.Collections.Generic;
using System.Linq;
using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.CoreServices {
    [Trait("Homepage", "User visits homepage")]
    public class ShowAllJokes : IntegrationTestBaseWithData {
        readonly JokeViewer viewer;
        public ShowAllJokes() {
            viewer = new JokeViewer(new Session());
        }
        [Fact(DisplayName = "Show all Jokes")]
        public void ShowAllJokesDefault() {
            List<Joke> result = viewer.ShowAllJokesHighestRatingFirst();
            Assert.Equal(3, result.Count());
        }
        [Fact(DisplayName = "Show all Jokes with highest rating first")]
        public void ShowListOfStoriesHighestRatingFirst() {
            List<Joke> result = viewer.ShowAllJokesHighestRatingFirst();
            // 10,2,5 is order of insert in db
            // First should be rating of 2
            Assert.Equal(10, result[0].Rating);
            Assert.Equal(5, result[1].Rating);
            Assert.Equal(2, result[2].Rating);
        }
    }
}
