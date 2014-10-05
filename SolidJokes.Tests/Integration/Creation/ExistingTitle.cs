using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using Xunit;

namespace SolidJokes.Tests.Integration.Creation {
    [Trait("JokeCreate", "Title has existing title")]
    public class ExistingTitle : TestBase {
        public ExistingTitle() {
            var app1 = new JokeApplication("asdf", "content", JokeType.Joke);
            new JokeCreator(new Session()).CreateOrEditJoke(app1);
        }

        [Fact(DisplayName = "JokeApplication is denied")]
        public void DoesNotThrow() {
            var app2 = new JokeApplication("asdf", "content", JokeType.Joke);
            Assert.DoesNotThrow(() => new JokeCreator(new Session()).CreateOrEditJoke(app2));
        }

        [Fact(DisplayName = "A message is shown to the administrator explaining why")]
        public void MessageShown() {
            var app2 = new JokeApplication("asdf", "content", JokeType.Joke);
            var result2 = new JokeCreator(new Session()).CreateOrEditJoke(app2);
            Assert.Contains("exists", result2.JokeApplication.Message);
        }
    }

}
