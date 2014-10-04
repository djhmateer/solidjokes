using System;
using SolidJokes.Core.Services;

namespace SolidJokes.Tests.Fakes
{
    public class FakeJokeVoter : IJokeVoter {
        public JokeVoterResult AddVote(int? jokeID) {
            throw new NotImplementedException();
        }
    }
}