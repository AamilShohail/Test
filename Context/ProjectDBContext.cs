using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Context
{
    public class ProjectDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This line will tell entity framework to use stored procedures
            // when inserting, updating and deleting Employees
            modelBuilder.Entity<User>().MapToStoredProcedures(proc =>
            proc.Insert(i => i.HasName("InsertUser")).
            Update(u => u.HasName("UpdateUser")).
            Delete(d => d.HasName("DeleteUser")));

            modelBuilder.Entity<Person>().MapToStoredProcedures(proc =>
            proc.Insert(i => i.HasName("InsertPerson")).
            Update(u => u.HasName("UpdatePerson")).
            Delete(d => d.HasName("DeletePerson")));


            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Test.Models.Login> Logins { get; set; }
    }
}