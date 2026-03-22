using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Opgave2_8.Models;

var projectDirectory = Directory.GetCurrentDirectory();
var dataDirectory = Path.Combine(projectDirectory, "Data");
var databasePath = Path.Combine(dataDirectory, "user-groups.db");
var setupScriptPath = Path.Combine(projectDirectory, "setup.sql");

EnsureDatabaseExists(dataDirectory, databasePath, setupScriptPath);

var options = new DbContextOptionsBuilder<UserGroupsContext>()
    .UseSqlite($"Data Source={databasePath}")
    .Options;

using var context = new UserGroupsContext(options);
var users = context.Users
    .Include(user => user.Groups)
    .OrderBy(user => user.Username)
    .ToList();

Console.WriteLine("Brugere og grupper:");

foreach (var user in users)
{
    var groupNames = string.Join(", ", user.Groups.OrderBy(group => group.Name).Select(group => group.Name));
    Console.WriteLine($"{user.Username}: {groupNames}");
}

void EnsureDatabaseExists(string directoryPath, string dbPath, string sqlPath)
{
    Directory.CreateDirectory(directoryPath);

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var setupSql = File.ReadAllText(sqlPath);
    using var command = connection.CreateCommand();
    command.CommandText = setupSql;
    command.ExecuteNonQuery();
}
