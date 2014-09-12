using Core.DB;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services {
    public class JokeViewer {
        public List<Joke> ShowAllJokesHighestRatingFirst() {
            List<Joke> result;
            using (var session = new Session()) {
                result = session.Jokes
                    .OrderByDescending(s => s.Rating)
                    .ToList();
            }
            return result;
        }
    }
}
