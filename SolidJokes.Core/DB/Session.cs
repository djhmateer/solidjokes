using System.Data.Entity;
using SolidJokes.Core.Models;

namespace SolidJokes.Core.DB {
    public interface ISession {
        DbSet<Joke> Jokes { get; set; }
        int SaveChanges();
    }

    public class Session : DbContext, ISession {
        public Session() : base(nameOrConnectionString: "SolidJokes") {
            // Nice for development
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Session>());
        }
        public DbSet<Joke> Jokes { get; set; }
    }
}
