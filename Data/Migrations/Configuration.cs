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
            var Universities = new List<University>
            {
                new University{UnivName="Université de Tunis"},
                new University{UnivName="Université de Tunis El-Manar "},
                new University{UnivName="Université de Carthage"},
                new University{UnivName="Université de Jendouba"},
                new University{UnivName="Université de Gabès"},
                new University{UnivName="Université de Sousse"},
                new University{UnivName="Université de Monastir"},
                new University{UnivName="Université de Sfax "},
                new University{UnivName="Université Zitouna"},
                new University{UnivName="Université de la Manouba"},
                new University{UnivName="Université de Kairouan "},
                new University{UnivName="Université de Gafsa"},
                new University{UnivName="Direction des ISET"},
                new University{UnivName="Université Virtuelle de Tunis"},
            };
            Universities.ForEach(e => context.University.AddOrUpdate(p => p.UnivName, e));
            context.SaveChanges();
        }
    }
}
