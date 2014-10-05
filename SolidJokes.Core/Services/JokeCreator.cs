using SolidJokes.Core.DB;
using SolidJokes.Core.Models;
using System;
using System.Linq;

namespace SolidJokes.Core.Services {
    public class JokeCreatorResult {
        public Joke NewJoke { get; set; }
        public JokeApplication JokeApplication { get; set; }

        public JokeCreatorResult() {
        }
    }

    public interface IJokeCreator {
        JokeCreatorResult InvalidApplication(string reason);
        JokeCreatorResult CreateOrEditJoke(JokeApplication app);
        Joke GetJokeByID(int id);
    }

    public class JokeCreator : IJokeCreator {
        private readonly ISession session;
        JokeApplication currentJokeApplication;

        public JokeCreator(ISession session) {
            this.session = session;
        }

        public Joke GetJokeByID(int id) {
            return session.Jokes.SingleOrDefault(x => x.ID == id);
        }

        bool TitleNotPresent() {
            return String.IsNullOrWhiteSpace(currentJokeApplication.Title);
        }

        bool TitleIsInvalid() {
            return currentJokeApplication.Title.Length < 4;
        }

        private bool TitleAlreadyExists() {
            bool exists = session.Jokes.FirstOrDefault(
                    s => s.Title == currentJokeApplication.Title
                    && s.ID != currentJokeApplication.JokeID) != null;
            return exists;
        }

        public JokeCreatorResult InvalidApplication(string reason) {
            var result = new JokeCreatorResult();
            currentJokeApplication.Status = JokeApplicationStatus.Invalid;
            result.JokeApplication = currentJokeApplication;
            result.JokeApplication.Message = reason;
            return result;
        }

        // Part 2
        private Joke AcceptApplication() {
            bool isEdit = currentJokeApplication.JokeID != 0;
            var story = new Joke();
            currentJokeApplication.Status = JokeApplicationStatus.Accepted;

            if (isEdit) {
                // Get existing Story
                story = session.Jokes.Find(currentJokeApplication.JokeID);
            }

            story.Title = currentJokeApplication.Title;
            story.Content = currentJokeApplication.Content;
            story.Rating = currentJokeApplication.Rating;
            // If CreatedAt is set then use it, else now
            story.CreatedAt = currentJokeApplication.CreatedAt ?? DateTime.Now;
            story.JokeType = currentJokeApplication.JokeType;
            story.VideoURL = currentJokeApplication.VideoURL;
            story.ImageURL = currentJokeApplication.ImageURL;

            if (!isEdit) {
                session.Jokes.Add(story);
            }

            session.SaveChanges();
            return story;
        }

        // Part 1
        public JokeCreatorResult CreateOrEditJoke(JokeApplication app) {
            bool isEdit = app.JokeID != 0;
            var result = new JokeCreatorResult();

            currentJokeApplication = app;
            result.JokeApplication = app;
            if (isEdit) {
                result.JokeApplication.Message = "Successfully edited joke!";
            } else {
                result.JokeApplication.Message = "Successfully created a new joke!";
            }

            if (TitleNotPresent())
                return InvalidApplication("Title is missing");

            if (TitleIsInvalid())
                return InvalidApplication("Title is invalid - needs to be 4 or more characters");

            if (TitleAlreadyExists())
                return InvalidApplication("Title exists already in database");

            // Accept the JokeApplication
            result.NewJoke = AcceptApplication();
            return result;
        }
    }
}
