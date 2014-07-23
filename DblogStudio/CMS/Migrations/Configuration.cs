namespace CMS.Migrations
{
    using CMS.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMS.Models.DblogStudioDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CMS.Models.DblogStudioDBContext";
        }

        protected override void Seed(CMS.Models.DblogStudioDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            List<Subject> listSubject = new List<Subject>();
            listSubject.Add(new Subject { Name = "Tin học đại cương - Khối xã hội", Group = "Đại cương" });
            listSubject.Add(new Subject { Name = "Tin học đại cương - Khối tự nhiên", Group = "Đại cương" });
            listSubject.Add(new Subject { Name = "Kỹ nghệ phần mềm", Group = "Cơ sở" });
            listSubject.Add(new Subject { Name = "Lập trình Web", Group = "Chuyên ngành" });
            listSubject.Add(new Subject { Name = ".Net Framework", Group = "Chuyên ngành" });
            listSubject.Add(new Subject { Name = "Lập trình di động", Group = "Chuyên ngành" });
            foreach (Subject s in listSubject)
                context.Subject.AddOrUpdate(s);
            context.SaveChanges();
        }
    }
}
