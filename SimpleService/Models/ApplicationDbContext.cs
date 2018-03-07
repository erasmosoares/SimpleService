using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MainObject> MainObjects { get; set; }
        public DbSet<Register> Registers { get; set; }
    }
}