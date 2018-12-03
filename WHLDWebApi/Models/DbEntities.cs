using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WHLDWebApi.Models
{
    public class DbEntities : DbContext
    {
        public DbEntities()
       : base("WHLDDB")
        {
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<XBInfo> XBInfos { get; set; }
        public DbSet<HistXBInfo> HistXBInfos { get; set; }
        public DbSet<Ghcs> Ghcss { get; set; }
        public DbSet<PlanGhcsRel> PlanGhcsRels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRoleRel> UserRoleRels { get; set; }
        public DbSet<RolePmsnRel> RolePmsnRels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}