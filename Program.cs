using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PM_2._0.Models;
using PM_2._0.Classes;

class Program
{
    static void Main(string[] args)
    {
        printIncompleteTasksAndTodos();
    }

    static void printIncompleteTasksAndTodos()
    {
        using (var context = new ProjectManagerContext())
        {
            var tasks = context.Tasks
                .Include(task => task.Todos)
                .Where(task => task.Todos.Any(todo => !todo.IsComplete))
                .ToList();

            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.TaskId} - {task.Name}");
                foreach (var todo in task.Todos.Where(t => !t.IsComplete))
                {
                    Console.WriteLine($"\tTodo: {todo.TodoId} - {todo.Name} (Incomplete)");
                }
            }
        }
    }
}