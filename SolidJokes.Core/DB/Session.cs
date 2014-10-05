using System.Data.Entity;
using SolidJokes.Core.Models;

namespace SolidJokes.Core.DB {
    public interface ISession {
        DbSet<Joke> Jokes { get; set; }
        //DbSet<Vote> Votes { get; set; }
        int SaveChanges();
    }

    // Account connection string is in Models/IdentityModel.cs
    public class Session : DbContext, ISession {
        public Session() : base(nameOrConnectionString: "SolidJokes") {
            // Nice for development - need migrations for this to work
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Session>());

            // Don't need migrations - useful if change the model, can recreate the db
            // run integration tests without the deletes to get data in again
            //Database.SetInitializer(new DropCreateDatabaseAlways<Session>());
        }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
