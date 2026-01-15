using Microsoft.EntityFrameworkCore;
using PM_2._0.Models;
using PM_2._0.Classes;
using ProjectTask = PM_2._0.Classes.Task;
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	static void Main(string[] args)
	{
		using var db = new BloggingContext();

		Console.WriteLine($"Database path: {db.DbPath}.");

		// Seed tasks and todos if not already present
		SeedTasks();

		// Fetch and display all tasks with their todos
		var tasks = db.Tasks
	.Include(t => t.Todos)
	.ToList();

		foreach (PM_2._0.Classes.Task task in tasks)
		{
			Console.WriteLine(task.Name);
			if (task.Todos != null)
			{
				foreach (var todo in task.Todos)
				{
					Console.WriteLine($"  - {todo.Name} (Done: {todo.IsComplete})");
				}
			}
		}
	}

	public static void SeedTasks()
	{
		using var db = new BloggingContext();
		if (!db.Tasks.Any())
		{
			var task1 = new ProjectTask
			{
				Name = "Produce software",
				Todos = new List<Todo>
			{
				new Todo { Name = "Write code" },
				new Todo { Name = "Compile source" },
				new Todo { Name = "Test program" }
			}
			};
			var task2 = new ProjectTask
			{
				Name = "Brew coffee",
				Todos = new List<Todo>
			{
				new Todo { Name = "Pour water" },
				new Todo { Name = "Pour coffee" },
				new Todo { Name = "Turn on" }
			}
			};
			db.Tasks.AddRange(task1, task2);
			db.SaveChanges();
		}
	}
}