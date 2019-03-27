namespace Data.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DatabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DatabContext context)
        {
        //    var University = new List<University>
        //    {
        //        new University{UnivName="Université de Tunis"},
        //        new University{UnivName="Université de Tunis El-Manar "},
        //        new University{UnivName="Université de Carthage"},
        //        new University{UnivName="Université de Jendouba"},
        //        new University{UnivName="Université de Gabès"},
        //        new University{UnivName="Université de Sousse"},
        //        new University{UnivName="Université de Monastir"},
        //        new University{UnivName="Université de Sfax "},
        //        new University{UnivName="Université Zitouna"},
        //        new University{UnivName="Université de la Manouba"},
        //        new University{UnivName="Université de Kairouan "},
        //        new University{UnivName="Université de Gafsa"},
        //        new University{UnivName="Direction des ISET"},
        //        new University{UnivName="Université Virtuelle de Tunis"},
        //    };
        //    University.ForEach(e => context.University.AddOrUpdate(p => p.UnivName, e));
        //    context.SaveChanges();

        //    var Admins = new List<Admin>
        //    {
        //        new Admin{nameAdmin="karim",mailAdmin="admin@admin.com",passwordAdmin="admin",isSuperAdmin=true},

        //    };
        //    Admins.ForEach(e => context.Admin.AddOrUpdate(c => c.mailAdmin, e));
        //    context.SaveChanges();


        //    var Organization = new List<organization>
        //    {

        //        new organization{orgname="Institut Supérieur de Gestion de Tunis",university=context.University.Find(1)},
        //        new organization{orgname="Institut Préparatoire aux Etudes d'Ingénieurs de Tunis",university=context.University.Find(1)},
        //        new organization{orgname="Ecole Normale Supérieure",university=context.University.Find(1)},
        //        new organization{orgname="Faculté des Sciences Economique et de Gestion de Tunis",university=context.University.Find(2)},
        //        new organization{orgname="Faculté des sciences de tunis",university=context.University.Find(2)},
        //        new organization{orgname="Ecole Nationale d'Ingénieurs de Tunis",university=context.University.Find(2)},
        //        new organization{orgname="Ecole Nationale des Sciences de l'Informatique",university=context.University.Find(10)},
        //    };
        //    Organization.ForEach(e => context.organization.AddOrUpdate(c => c.orgname, e));
        //    context.SaveChanges();

        }


       

    }
}

