using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
        public DbSet<Ghcs> Ghcss { get; set; }
        public DbSet<PlanGhcsRel> PlanGhcsRels { get; set; }
    }
}