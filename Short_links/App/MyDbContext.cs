using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Short_links.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Short_links.App
{
    public class MyDbContext: DbContext
    {   
        public  DbSet<Urls> Urls { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "SQLiteURL.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }


    }
}
