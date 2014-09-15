using Core.Models;
using System.Data.Entity;

namespace Core.DB {
    public interface ISession {
        DbSet<Joke> Jokes { get; set; }
        int SaveChanges();
    }

    public class Session : DbContext, ISession {
        public Session() : base(nameOrConnectionString: "Funny") { }
        public DbSet<Joke> Jokes { get; set; }
    }
}
