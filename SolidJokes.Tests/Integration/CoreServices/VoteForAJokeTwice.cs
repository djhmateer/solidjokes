using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidJokes.Core.DB;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.CoreServices {
    [Trait("Homepage", "User votes for a Joke twice in the space of 10 seconds")]
    public class VoteForAJokeTwice : IntegrationTestBaseWithData {
       readonly JokeVoter jokeVoter;
       public VoteForAJokeTwice() {
            jokeVoter = new JokeVoter(new Session());
        }

        [Fact(DisplayName = "Rating is not incremented by 1")]
        public void RatingIsNotIncrementedBy1() {
           var session = new Session();
           var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");
           Assert.Equal(10, bananaJoke.Rating);
           var result = jokeVoter.AddVote(bananaJoke.ID);
           Assert.Equal(11, result.Joke.Rating);

           result = jokeVoter.AddVote(bananaJoke.ID);
           Assert.Equal(11, result.Joke.Rating);
        }

        [Fact(DisplayName = "A red messsage is provided to the User")]
        public void AMessageIsProvidedForUser() {
            var session = new Session();
            var bananaJoke = session.Jokes.FirstOrDefault(x => x.Title == "Banana");
            Assert.Equal(10, bananaJoke.Rating);
            var result = jokeVoter.AddVote(bananaJoke.ID);
            Assert.Equal(11, result.Joke.Rating);

            result = jokeVoter.AddVote(bananaJoke.ID);
            Assert.False(result.VoteSucceeded);
            Assert.Equal("Only 1 vote per story allowed every 10 seconds :-)", result.Message);
        }
    }
}
