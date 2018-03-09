using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    /// <summary>
    /// Databse Contexts
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Register> Registers { get; set; }
        public DbSet<JSONFile> Files { get; set; }

    }
}