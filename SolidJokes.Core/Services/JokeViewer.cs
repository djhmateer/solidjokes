using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace SolidJokes.Core.Services {

    public interface IJokeViewer {
        List<Joke> ShowAllJokesHighestRatingFirst();
        List<Joke> ShowAllJokesByDateCreatedDescending();
        Joke AddJoke(string title, int rating);
        Joke GetJokeByID(int id);
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

        public List<Joke> ShowAllJokesByDateCreatedDescending() {
            return session.Jokes
                    .OrderByDescending(s => s.CreatedAt)
                    .ToList();
        }

        public Joke GetJokeByID(int id) {
            return session.Jokes.SingleOrDefault(x => x.ID == id);
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
