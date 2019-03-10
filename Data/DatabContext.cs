using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
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
        public DbSet<EventPicture> EventPictures { get; set; }
        public DbSet<organization> organization { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }


    }
}




