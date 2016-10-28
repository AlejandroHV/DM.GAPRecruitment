using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.Model.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DM.GAPRecruitment.DAL.Context
{
    public class SuperZapatosContext : DbContext , IContext
    {
        public SuperZapatosContext() : base("name=SuperZapatosConnection")
        {


        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            int a = 0;
            try
            {
                a= base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                
            }
            return a;
           
        }

    }
}
