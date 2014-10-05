using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidJokes.Core.DB;
using SolidJokes.Core.Models;

namespace SolidJokes.Core.Services {
    public class JokeCreatorResult {
        public Joke NewJoke { get; set; }
        public JokeApplication JokeApplication { get; set; }

        public JokeCreatorResult() {
        }
    }

    public interface IJokeCreator
    {
        JokeCreatorResult InvalidApplication(string reason);
        JokeCreatorResult CreateOrEditJoke(JokeApplication app);
    }

    public class JokeCreator : IJokeCreator
    {
        private readonly ISession session;
        JokeApplication CurrentApplication;

        public JokeCreator(ISession session) {
            this.session = session;
        }

        bool TitleNotPresent() {
            return String.IsNullOrWhiteSpace(CurrentApplication.Title);
        }

        bool TitleIsInvalid() {
            return CurrentApplication.Title.Length < 4;
        }

        private bool TitleAlreadyExists() {
            bool exists = session.Jokes.FirstOrDefault(
                    s => s.Title == CurrentApplication.Title
                    && s.ID != CurrentApplication.JokeID) != null;
            return exists;
        }

        public JokeCreatorResult InvalidApplication(string reason) {
            var result = new JokeCreatorResult();
            CurrentApplication.Status = JokeApplicationStatus.Invalid;
            result.JokeApplication = CurrentApplication;
            result.JokeApplication.Message = reason;
            return result;
        }

        // Part 2
        private Joke AcceptApplication() {
            bool isEdit = CurrentApplication.JokeID != 0;
            var story = new Joke();
            CurrentApplication.Status = JokeApplicationStatus.Accepted;

            if (isEdit) {
                // Get existing Story
                story = session.Jokes.Find(CurrentApplication.JokeID);
            }

            story.Title = CurrentApplication.Title;
            story.Content = CurrentApplication.Content;
            story.Rating = CurrentApplication.Rating;
            // If CreatedAt is set then use it, else now
            story.CreatedAt = CurrentApplication.CreatedAt ?? DateTime.Now;
            story.JokeType = CurrentApplication.JokeType;
            story.VideoURL = CurrentApplication.VideoURL;
            story.ImageURL = CurrentApplication.ImageURL;

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

            CurrentApplication = app;
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
