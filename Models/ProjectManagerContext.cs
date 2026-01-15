using System;
using System.Collections.Generic;
using System.Text;

namespace PM_2._0.Models
{
	using Microsoft.EntityFrameworkCore;
    using PM_2._0.Classes;
    using System;
	using System.Collections.Generic;

	public class BloggingContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<Todo> Todos { get; set; }

		public string DbPath { get; }

		public BloggingContext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "projects.db");
		}

		// The following configures EF to create a Sqlite database file in the
		// special "local" folder for your platform.
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}

	public class Blog
	{
		public int BlogId { get; set; }
		public string Url { get; set; }

		public List<Post> Posts { get; } = new();
	}

	public class Post
	{
		public int PostId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

		public int BlogId { get; set; }
		public Blog Blog { get; set; }
	}
}
