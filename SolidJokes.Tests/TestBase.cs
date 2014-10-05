using SolidJokes.Core.DB;
using System;

namespace SolidJokes.Tests {
    public class TestBase : IDisposable {
        public TestBase() {
            new Session().Database
                .ExecuteSqlCommand("DELETE FROM Votes; DELETE FROM Jokes");
        }

        public void Dispose() {
            new Session().Database
                .ExecuteSqlCommand("DELETE FROM Votes; DELETE FROM Jokes");
        }
    }
}
