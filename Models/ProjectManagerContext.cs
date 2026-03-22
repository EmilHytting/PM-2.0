using System;
using Microsoft.EntityFrameworkCore;
using PM_2._0.Classes;
using ProjectTask = PM_2._0.Classes.Task;

namespace PM_2._0.Models
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<TeamWorker> TeamWorkers { get; set; }

        public string DbPath { get; }

        public ProjectManagerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "projects.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>()
                .HasOne(task => task.Team)
                .WithMany(team => team.Tasks)
                .HasForeignKey(task => task.TeamId);

            modelBuilder.Entity<Todo>()
                .HasOne(todo => todo.Task)
                .WithMany(task => task.Todos)
                .HasForeignKey(todo => todo.TaskId);

            modelBuilder.Entity<Todo>()
                .HasOne(todo => todo.Worker)
                .WithMany(worker => worker.Todos)
                .HasForeignKey(todo => todo.WorkerId);

            modelBuilder.Entity<Team>()
                .HasOne(team => team.CurrentTask)
                .WithMany()
                .HasForeignKey(team => team.CurrentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Worker>()
                .HasOne(worker => worker.CurrentTodo)
                .WithMany()
                .HasForeignKey(worker => worker.CurrentTodoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamWorker>()
                .HasKey(teamWorker => new { teamWorker.TeamId, teamWorker.WorkerId });

            modelBuilder.Entity<TeamWorker>()
                .HasOne(teamWorker => teamWorker.Team)
                .WithMany(team => team.Workers)
                .HasForeignKey(teamWorker => teamWorker.TeamId);

            modelBuilder.Entity<TeamWorker>()
                .HasOne(teamWorker => teamWorker.Worker)
                .WithMany(worker => worker.Teams)
                .HasForeignKey(teamWorker => teamWorker.WorkerId);
        }
    }
}
