using System;

namespace SolidJokes.Core.Models {
    public class Vote {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Joke Joke { get; set; }
    }
}
