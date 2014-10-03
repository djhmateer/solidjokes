using System.Data.Entity;
using SolidJokes.Core.DB;
using SolidJokes.Core.Models;

namespace SolidJokes.Tests.Fakes
{
    public class FakeSession : ISession {
        public DbSet<Joke> Jokes { get; set; }
        public int SaveChangesCount { get; private set; }

        public FakeSession() {
            this.Jokes = new TestDbSet<Joke>();
            //this.Jokes = new TestJokeDbSet();
        }

        public int SaveChanges() {
            this.SaveChangesCount++;
            return 1;
        }
    }

    //The Find method is difficult to implement in a generic fashion. 
    //If you need to test code that makes use of the Find method it is easiest to create a test DbSet for each of the entity types that need to support find
    //public class TestJokeDbSet : TestDbSet<Joke> {
    //    public override Joke Find(params object[] keyValues) {
    //        var id = (int)keyValues.Single();
    //        return this.SingleOrDefault(b => b.ID == id);
    //    }
    //} 

}