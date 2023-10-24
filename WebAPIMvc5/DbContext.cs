using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIMvc5.Models;
using System.Data.Entity;


namespace WebAPIMvc5
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}