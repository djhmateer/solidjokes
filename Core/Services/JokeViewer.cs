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

        public List<Joke> ShowAllJokesHighestRatingFirst() {
            //return session.Jokes
            //    .OrderByDescending(s => s.Rating)
            //    .ToList();

            // something wrong in test implementation maybe..have to call tolist here.
            var list = session.Jokes.ToList();
            //var list = session.Jokes;
            var orderedList = list.OrderByDescending(x => x.Rating).ToList();
            return orderedList;
        }
    }
}
