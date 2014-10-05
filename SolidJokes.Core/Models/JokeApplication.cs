using System;
using System.ComponentModel.DataAnnotations;

namespace SolidJokes.Core.Models {
    public enum JokeApplicationStatus {
        Pending,
        Validated,
        Invalid,
        Accepted,
        Denied
    }

    public class JokeApplication {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Content { get; set; }
        public JokeType JokeType { get; set; }
        public JokeApplicationStatus Status { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public string VideoURL { get; set; }
        public string ImageURL { get; set; }
        public int JokeID { get; set; }
        // To allow date importer to set the date
        public DateTime? CreatedAt { get; set; }

        public JokeApplication() { }

        // Helper so don't need to pass JokeID (as we do when editing) and a rating
        public JokeApplication(string title, string content, JokeType jokeType) :
            this(title, content, jokeType, null, null, 0, 0) {
        }

        public JokeApplication(string title, string content, JokeType jokeType,
            string imageUrl, string videoUrl, int jokeID, int rating) {

            this.Title = title;
            this.Content = content;
            this.JokeType = jokeType;
            this.Status = JokeApplicationStatus.Pending;
            this.JokeID = jokeID;
            this.Rating = rating;
            this.ImageURL = ImageURL;
            this.VideoURL = videoUrl;

            // Caught by dataannotation js validations in web project, however if using a Console then this would throw
            if (String.IsNullOrWhiteSpace(this.Title)
               || String.IsNullOrWhiteSpace(this.Content))
                throw new InvalidOperationException("Can't have an empty Title or Content");
        }

        public bool IsAccepted() {
            return this.Status == JokeApplicationStatus.Accepted;
        }

        public bool IsValid() {
            return this.Status == JokeApplicationStatus.Validated ||
              this.Status == JokeApplicationStatus.Accepted;
        }

        public bool IsInvalid() {
            return !IsValid();
        }
    }
}
