using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.DB {
    public class Session : DbContext {
        public Session()
            : base(nameOrConnectionString: "Funny") {
            // Nice for development
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Session>());
        }
        public DbSet<Story> Stories { get; set; }
        //public DbSet<Vote> Votes { get; set; }
    }
}
