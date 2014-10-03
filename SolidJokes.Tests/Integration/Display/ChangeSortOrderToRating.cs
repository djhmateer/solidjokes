using System.Collections.Generic;
using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.Display {
    [Trait("Homepage", "User changes sort order to highest rating first")]
    public class ChangeSortOrderToRating : IntegrationTestBaseWithData {
        readonly JokeViewer viewer;
        public ChangeSortOrderToRating() {
            viewer = new JokeViewer(new Session());
        }

        [Fact(DisplayName = "Show all 3 Stories")]
        public void ShowAllStories() {
            List<Joke> result = viewer.ShowAllJokesHighestRatingFirst();
            Assert.Equal(3, result.Count);
        }
        [Fact(DisplayName = "Show all Stories in rating order")]
        public void ShowListOfStoriesInDescendingRankOrder() {
            var result = viewer.ShowAllJokesHighestRatingFirst();
            // 10,2,5 is order of insert in db
            // First should be rating of 2
            Assert.Equal(10, result[0].Rating);
            Assert.Equal(5, result[1].Rating);
            Assert.Equal(2, result[2].Rating);
        }
    }
}
