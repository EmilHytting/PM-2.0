using System;
using Microsoft.EntityFrameworkCore;
using PM_2._0.Classes;

namespace PM_2._0.Models
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<PM_2._0.Classes.Task> Tasks { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public string DbPath { get; }

        public ProjectManagerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "projects.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
