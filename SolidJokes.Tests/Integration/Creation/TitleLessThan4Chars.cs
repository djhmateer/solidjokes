using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.Creation {
    [Trait("JokeCreate", "Title <= 4 characters")]
    public class ShortTitle : TestBase {
        readonly JokeCreatorResult jokeCreatorResult;
        public ShortTitle() {
            var app = new JokeApplication("asd", "content", JokeType.Joke);
            jokeCreatorResult = new JokeCreator(new Session()).CreateOrEditJoke(app);
        }

        [Fact(DisplayName = "JokeApplication is denied")]
        public void JokeApplicaitonDenied() {
            Assert.False(jokeCreatorResult.JokeApplication.IsValid());
        }

        [Fact(DisplayName = "A message is shown to the administrator explaining why")]
        public void MessageShown() {
            Assert.Contains("invalid", jokeCreatorResult.JokeApplication.Message);
        }
    }
}
