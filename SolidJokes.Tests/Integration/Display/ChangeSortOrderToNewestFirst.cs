using Core.DB;
using Core.Models;
using Core.Services;
using System.Collections.Generic;
using Xunit;

namespace SolidJokes.Tests.Integration.Display {
    [Trait("Homepage", "User changes date sort to newest first")]
    public class ChangeSortOrderToNewestFirst : IntegrationTestBaseWithData {
        readonly JokeViewer viewer;
        public ChangeSortOrderToNewestFirst() {
            viewer = new JokeViewer(new Session());
        }

        [Fact(DisplayName = "Show all 3 Stories")]
        public void ShowAllStories() {
            List<Joke> result = viewer.ShowAllJokesByDateCreatedDescending();
            Assert.Equal(3, result.Count);
        }
        [Fact(DisplayName = "Show all Stories newest first")]
        public void ShowListOfStoriesInDescendingDateCreatedOrder() {
            var result = viewer.ShowAllJokesByDateCreatedDescending();
            // 10,2,5 is order of insert in db
            // First should be rating of 2
            Assert.Equal("Banana", result[0].Title);
            Assert.Equal("Pizza", result[1].Title);
            Assert.Equal("Lily", result[2].Title);
        }
    }
}
