using Core.DB;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services {

    public interface IJokeViewer {
        List<Joke> ShowAllJokesHighestRatingFirst();
        Joke AddJoke(string title, int rating);
    }
    public class JokeViewer : IJokeViewer {
        private readonly ISession session;
        public JokeViewer(ISession session) {
            this.session = session;
        }

        public List<Joke> ShowAllJokesHighestRatingFirst() {
            return session.Jokes
                .OrderByDescending(s => s.Rating)
                .ToList();
        }

        // Should be in another object - JokeAdder?
        public Joke AddJoke(string title, int rating) {
            var joke = new Joke { Title = title, Rating = rating };
            session.Jokes.Add(joke);
            session.SaveChanges();
            return joke;
        }
    }
}
