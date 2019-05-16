using System.Data.Entity;
using Model;


namespace Data
{
    public class DatabContext:DbContext

    {
        public DatabContext():base("Name=Event")
        {
          
          
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventPicture> EventPicture { get; set; }
        public DbSet<organization> organization { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }
        public DbSet<Logs> Logs { get; set; }


    }
}




