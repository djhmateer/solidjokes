using System;

namespace SolidJokes.Core.Models {
    public class Joke {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public JokeType JokeType { get; set; }

        public Joke() {
            //this.CreatedAt = DateTime.Now;
            this.JokeType = JokeType.Joke;
        }
    }
}
