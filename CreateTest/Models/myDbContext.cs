using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CreateTest.Models
{
    public class myDbContext : IdentityDbContext<ApplicationUser>
    {
        public myDbContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TopTenList> TopTenList { get; set; }
        public DbSet<ListItem> List_Items { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rated> RatedList { get; set; }
        public DbSet<ListUserHasRated> UserRatedLists { get; set; }
        
    }
}