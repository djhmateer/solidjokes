using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using System;
using System.Linq;

namespace SolidJokes.Core.Services {
    public class JokeVoterResult {
        public Joke Joke { get; set; }
        public String Message { get; set; }
        public bool VoteSucceeded { get; set; }
    }

    public interface IJokeVoter {
        JokeVoterResult AddVote(int? jokeID);
    }

    public class JokeVoter : IJokeVoter {
        private readonly ISession session;

        public JokeVoter(ISession session) {
            this.session = session;
        }

        public JokeVoterResult AddVote(int? jokeID) {
            var result = new JokeVoterResult();
            var joke = session.Jokes.Find(jokeID);

            // Has this joke been voted for in the last 10 seconds?
            var votes = joke.Votes
                .FirstOrDefault(v => v.CreatedAt > DateTime.Now.AddSeconds(-10));

            if (votes == null) {
                // Success!
                result.VoteSucceeded = true;

                joke.Votes.Add(new Vote { CreatedAt = DateTime.Now });

                var currentRating = joke.Rating;
                joke.Rating = currentRating + 1;

                result.Joke = joke;
                result.Message = "Thank you for voting!";
                session.SaveChanges();
            } else {
                result.VoteSucceeded = false;
                result.Joke = joke;
                result.Message = "Only 1 vote per story allowed every 10 seconds :-)";
            }
            return result;
        }
    }
}
