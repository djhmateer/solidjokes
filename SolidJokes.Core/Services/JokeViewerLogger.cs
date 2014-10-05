using System.Collections.Generic;
using System.Diagnostics;
using SolidJokes.Core.Models;

namespace SolidJokes.Core.Services {
    public class JokeViewerLogger : IJokeViewer {
        private readonly IJokeViewer jokeViewer;

        public JokeViewerLogger(IJokeViewer jokeViewer) {
            this.jokeViewer = jokeViewer;
        }

        public List<Joke> ShowAllJokesHighestRatingFirst() {
            Debug.WriteLine("In JokeViewer - ShowAllJokesHighestRatingFirst");
            var result = jokeViewer.ShowAllJokesHighestRatingFirst();
            Debug.WriteLine("End ShowAllJokesHighestRatingFirst");
            return result;
        }

        public Joke AddJoke(string title, int rating) {
            var result = jokeViewer.AddJoke(title, rating);
            return result;
        }

        public Joke GetJokeByID(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Joke> ShowAllJokesByDateCreatedDescending() {
            return jokeViewer.ShowAllJokesByDateCreatedDescending();
        }
    }
}