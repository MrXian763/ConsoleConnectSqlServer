using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer.EF6
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("Server=localhost;Database=TestDB;Trusted_Connection=true;")
        {

        }

        public DbSet<UserTModelForEF> UserT { get; set; }
        public DbSet<UserScoresTModelForEF> UserScoresT { get; set; }
    }
}
