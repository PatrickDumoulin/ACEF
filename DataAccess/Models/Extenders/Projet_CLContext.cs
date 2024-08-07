using DataAccess.Core.Definitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class Projet_CLContext : IDbContext
    {
        string connectionString; 

        public Projet_CLContext(string connectionString) : base(getOptions(connectionString))
        {
           this.connectionString = connectionString;
        }

        public event EventHandler OnSaveException;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(connectionString);


        private static DbContextOptions getOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
