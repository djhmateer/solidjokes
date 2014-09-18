using Core.Models;
using Core.Services;
using System;
using System.Linq;
using Xunit;

namespace SolidJokes.Tests {
    public class JokeViewerTests {
        [Fact]
        public void ShowAllJokesHighestRatingFirst_AFakeSessionWithListOfJokes_ShouldReturnListInRatingOrder() {
            var session = new FakeSession();
            session.Jokes.Add(new Joke { ID = 1, Rating = 2, Title = "Banana"});
            session.Jokes.Add(new Joke { ID = 2, Rating = 9, Title = "Stick"});
            session.Jokes.Add(new Joke { ID = 3, Rating = 6, Title = "Airport"});

            var viewer = new JokeViewer(session);
            var result = viewer.ShowAllJokesHighestRatingFirst();
            Assert.Equal("Stick", result[0].Title);
            Assert.Equal("Airport", result[1].Title);
            Assert.Equal("Banana", result[2].Title);
        }

        [Fact]
        public void ShowAllJokesByDateCreatedDescending_AFakeSessionWithListOfJokesWithDateCreatedDesc_ShouldReturnNewestJokesFirst() {
            var session = new FakeSession();
            session.Jokes.Add(new Joke { ID = 1, Rating = 2, Title = "Banana", CreatedAt = DateTime.Now});
            session.Jokes.Add(new Joke { ID = 2, Rating = 9, Title = "Stick", CreatedAt = DateTime.Now.AddDays(-3) });
            session.Jokes.Add(new Joke { ID = 3, Rating = 6, Title = "Airport", CreatedAt = DateTime.Now.AddDays(-2)});

            var viewer = new JokeViewer(session);
            var result = viewer.ShowAllJokesByDateCreatedDescending();
            Assert.Equal("Banana", result[0].Title);
            Assert.Equal("Airport", result[1].Title);
            Assert.Equal("Stick", result[2].Title);
        }

        [Fact]
        public void AddJoke_GivenTitleAndRating_ShouldSaveToDbAndBeAvailableInTheSession()
        {
            var session = new FakeSession();
            var viewer = new JokeViewer(session);

            var result = viewer.AddJoke("sausage", 2);

            Assert.Equal(1, session.Jokes.Count());
            Assert.Equal("sausage", session.Jokes.Single().Title);
            Assert.Equal(2, session.Jokes.Single().Rating);
            Assert.Equal(1, session.SaveChangesCount);
        }
    }
}