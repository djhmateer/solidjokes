using Core.Models;
using System.Data.Entity;

namespace Core.DB {
    public class Session : DbContext {
        public Session() : base(nameOrConnectionString: "Funny") {}
        public DbSet<Joke> Jokes { get; set; }
    }
}
