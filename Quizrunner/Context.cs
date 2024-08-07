using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{
    public class Context : DbContext
    {
        public DbSet<Tests_bd> Tests_bd { get; set; }
        public DbSet<TestAnswers> TestAnswers { get; set; }
        public DbSet<TestQuestions> TestQuestions { get; set; }  
        public DbSet<Users> Users { get; set; }
        public DbSet<Results> Results { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(basePath).Parent.Parent.Parent.FullName;
            string databasePath = Path.Combine(projectDirectory, "TrpoDB.db");

            optionsBuilder.UseSqlite($"DataSource={databasePath}");
        }
    }
}
