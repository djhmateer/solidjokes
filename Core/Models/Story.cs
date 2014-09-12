using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Story {
        public int ID { get; set; }
        //[MaxLength(255)]
        //[Required]
        public string Title { get; set; }
        //[MaxLength(2048)]
        public string Content { get; set; }
        public int Rating { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public StoryType StoryType { get; set; }
        //public virtual ICollection<Vote> Votes { get; set; }

        public Story() {
            this.CreatedAt = DateTime.Now;
            this.StoryType = StoryType.Joke;
        }
    }
}
