using Core.Models;
using System.Data.Entity;

namespace Core.DB {
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
