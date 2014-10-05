using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using System;
using Xunit;

namespace SolidJokes.Tests.Integration.Creation {
    [Trait("JokeEdit", "Valid edit of existing Joke")]
    public class EditJokeReceived : TestBase {
        readonly JokeCreatorResult jokeCreatorResult;
        readonly Joke joke2;

        public EditJokeReceived() {
            // Create a Joke
            var jokeCreator = new JokeCreator(new Session());
            var app = new JokeApplication("Stick", "Whats brown and sticky? A stick", JokeType.Joke);
            jokeCreatorResult = jokeCreator.CreateOrEditJoke(app);

            // Edit that Joke using the id created above
            var jokeCreator2 = new JokeCreator(new Session());
            var app2 = new JokeApplication("Stick", "Whats brown and sticky? A stick",
                              JokeType.Joke, "", "", jokeID: jokeCreatorResult.NewJoke.ID, rating: 5);
            var result2 = jokeCreator2.CreateOrEditJoke(app2);
            joke2 = result2.NewJoke;
        }

        [Fact(DisplayName = "Joke CreatedAt date is changed to now")]
        public void JokeCreatedAt() {
            var dbMinute = joke2.CreatedAt.Minute;
            Assert.Equal(DateTime.Now.Minute, dbMinute);
        }
        [Fact(DisplayName = "Rating remains the same")]
        public void RatingRemainsTheSame() {
            var rating = joke2.Rating;
            Assert.Equal(5, rating);
        }
        [Fact(DisplayName = "Joke is validated")]
        public void JokeValidated() {
            Assert.True(jokeCreatorResult.JokeApplication.IsValid());
        }
        [Fact(DisplayName = "Joke is accepted")]
        public void JokeAccepted() {
            Assert.True(jokeCreatorResult.JokeApplication.IsAccepted());
        }
        [Fact(DisplayName = "Joke is added to the system")]
        public void JokeAddedToSystem() {
            Assert.NotNull(jokeCreatorResult);
        }
        [Fact(DisplayName = "A JokeType of Joke is the default")]
        public void JokeTypeJoke() {
            Assert.Equal(JokeType.Joke, joke2.JokeType);
        }
        [Fact(DisplayName = "A confirmation message is shown to the administrator")]
        public void ConfirmationMessage() {
            Assert.Equal("Successfully created a new joke!", jokeCreatorResult.JokeApplication.Message);
        }
    }
}
