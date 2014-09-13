using Core.DB;
using Core.Models;
using Core.Services;
using System.Data.Entity;
using Xunit;

namespace SolidJokes.Tests {
    public class JokeViewerTests {
        [Fact]
        public void Index() {
            var session = new FakeSession();
            var viewer = new JokeViewer(session);

            var result = viewer.ShowAllJokesHighestRatingFirst();

            Assert.NotEmpty(result);
        }
    }

    public class FakeSession : ISession {
        public DbSet<Joke> Jokes { get; set; }
    }
}