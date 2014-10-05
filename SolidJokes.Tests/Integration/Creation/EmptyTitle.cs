using SolidJokes.Core.Models;
using System;
using Xunit;

namespace SolidJokes.Tests.Integration.Creation {
    [Trait("JokeCreate", "Empty Title or Content")]
    public class EmptyTitle : TestBase {

        [Fact(DisplayName = "An exception is thrown with empty title")]
        public void ApplicationInvalid() {
            Assert.Throws<InvalidOperationException>(
                    () => new JokeApplication("", "content here", JokeType.Joke)
                );
        }
        [Fact(DisplayName = "An exception is thrown with empty password")]
        public void MessageReturned() {
            Assert.Throws<InvalidOperationException>(
                     () => new JokeApplication("title here", "", JokeType.Joke)
                 );
        }
    }
}
