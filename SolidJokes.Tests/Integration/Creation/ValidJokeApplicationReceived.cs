using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.Creation {
    [Trait("JokeCreate", "Valid JokeApplication")]
    public class ValidJokeApplicationReceived : TestBase {
        readonly JokeCreatorResult jokeCreateResult;
        readonly Joke joke;

        public ValidJokeApplicationReceived() {
            var jokeCreator = new JokeCreator(new Session());
            var application = new JokeApplication("Stick", "Whats brown and sticky? A stick", JokeType.Joke);
            jokeCreateResult = jokeCreator.CreateOrEditJoke(application);
            joke = jokeCreateResult.NewJoke;
        }

        [Fact(DisplayName = "Joke is validated")]
        public void JokeValidated() {
            Assert.True(jokeCreateResult.JokeApplication.IsValid());
        }
        [Fact(DisplayName = "Joke is accepted")]
        public void JokeAccepted() {
            Assert.True(jokeCreateResult.JokeApplication.IsAccepted());
        }
        [Fact(DisplayName = "Joke is added to the system")]
        public void JokeAddedToSystem() {
            Assert.NotNull(jokeCreateResult);
        }
        [Fact(DisplayName = "A JokeType of Joke is the default")]
        public void JokeTypeJoke() {
            Assert.Equal(JokeType.Joke, joke.JokeType);
        }
        [Fact(DisplayName = "A confirmation message is shown to the administrator")]
        public void ConfirmationMessage() {
            Assert.Equal("Successfully created a new joke!", jokeCreateResult.JokeApplication.Message);
        }
    }

    //[Trait("JokeCreate", "Valid Video JokeApplication")]
    //public class ValidVideoJokeApplicationReceived : TestBase {
    //    JokeCreatorResult _result;
    //    Joke _Joke;

    //    public ValidVideoJokeApplicationReceived() {
    //        var JokeCreator = new JokeCreator();
    //        var application = new JokeApplication("Stick", "Whats brown and sticky? A stick", JokeType.Video, null, "http://www.google.co.uk", 0, 14);
    //        _result = JokeCreator.CreateOrEditJoke(application);
    //        _Joke = _result.NewJoke;
    //    }

    //    [Fact(DisplayName = "Joke is validated")]
    //    public void JokeValidated() {
    //        Assert.True(_result.JokeApplication.IsValid());
    //    }
    //    [Fact(DisplayName = "Joke is accepted")]
    //    public void JokeAccepted() {
    //        Assert.True(_result.JokeApplication.IsAccepted());
    //    }
    //    [Fact(DisplayName = "Joke is added to the system")]
    //    public void JokeAddedToSystem() {
    //        Assert.NotNull(_result);
    //    }
    //    [Fact(DisplayName = "A JokeType of Video")]
    //    public void JokeTypeJoke() {
    //        Assert.Equal(JokeType.Video, _Joke.JokeType);
    //    }
    //    [Fact(DisplayName = "A confirmation message is shown to the administrator")]
    //    public void ConfirmationMessage() {
    //        Assert.Equal("Successfully created a new Joke!", _result.JokeApplication.Message);
    //    }
    //}
}
