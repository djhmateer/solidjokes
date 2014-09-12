using Core.DB;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services {

    public interface IJokeViewer { List<Joke> ShowAllJokesHighestRatingFirst();}
    public class JokeViewer : IJokeViewer {
        private readonly ISession session;
        public JokeViewer(ISession session) {
            this.session = session;
        }

        public List<Joke> ShowAllJokesHighestRatingFirst()
        {
            var result = session.Jokes
                .OrderByDescending(s => s.Rating)
                .ToList();
            return result;
        }
    }
}
