using System;
using System.Collections.Generic;
using Core.Models;
using Core.Services;

namespace SolidJokes.Tests.Fakes
{
    public class FakeJokeViewer : IJokeViewer {
        public List<Joke> ShowAllJokesHighestRatingFirst() {
            return new List<Joke> { new Joke(), new Joke() };
        }

        public Joke AddJoke(string title, int rating) {
            throw new NotImplementedException();
            //return new Joke { Title = title, Rating = rating };
        }
    }
}