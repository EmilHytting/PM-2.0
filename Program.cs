using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PM_2._0.Classes;
using PM_2._0.Models;
using ProjectTask = PM_2._0.Classes.Task;

class Program
{
    static void Main(string[] args)
    {
        using var context = new ProjectManagerContext();

        context.Database.Migrate();
        SeedData(context);
        EnsureTeamWithoutTasks(context);
        PrintTeamOverview(context);
        PrintTeamsWithoutTasks();
    }

    static void SeedData(ProjectManagerContext context)
    {
        if (context.Teams.Any())
        {
            return;
        }

        var sofie = new Worker { Name = "Sofie" };
        var jonas = new Worker { Name = "Jonas" };
        var asta = new Worker { Name = "Asta" };

        var platformTeam = new Team { Name = "Platform-team" };
        var onboardingTeam = new Team { Name = "Onboarding-team" };

        context.AddRange(sofie, jonas, asta, platformTeam, onboardingTeam);
        context.AddRange(
            new TeamWorker { Team = platformTeam, Worker = sofie },
            new TeamWorker { Team = platformTeam, Worker = jonas },
            new TeamWorker { Team = onboardingTeam, Worker = sofie },
            new TeamWorker { Team = onboardingTeam, Worker = asta });
        context.SaveChanges();

        var stabiliserLoginflow = new ProjectTask
        {
            Name = "Stabiliser loginflow",
            Team = platformTeam
        };

        var klarOnboarding = new ProjectTask
        {
            Name = "Klar onboarding for nye kunder",
            Team = onboardingTeam
        };

        var retTimeout = new Todo
        {
            Name = "Ret timeout pa session",
            IsComplete = false,
            Task = stabiliserLoginflow,
            Worker = jonas
        };

        var gennemgaValideringsfejl = new Todo
        {
            Name = "Gennemga valideringsfejl",
            IsComplete = true,
            Task = stabiliserLoginflow,
            Worker = sofie
        };

        var skrivVelkomstguide = new Todo
        {
            Name = "Skriv velkomstguide",
            IsComplete = false,
            Task = klarOnboarding,
            Worker = asta
        };

        var opdaterStartskaerm = new Todo
        {
            Name = "Opdater startskaerm",
            IsComplete = false,
            Task = klarOnboarding,
            Worker = sofie
        };

        context.AddRange(
            stabiliserLoginflow,
            klarOnboarding,
            retTimeout,
            gennemgaValideringsfejl,
            skrivVelkomstguide,
            opdaterStartskaerm);
        context.SaveChanges();

        platformTeam.CurrentTask = stabiliserLoginflow;
        onboardingTeam.CurrentTask = klarOnboarding;

        sofie.CurrentTodo = opdaterStartskaerm;
        jonas.CurrentTodo = retTimeout;
        asta.CurrentTodo = skrivVelkomstguide;

        context.SaveChanges();
    }

    static void PrintTeamOverview(ProjectManagerContext context)
    {
        var teams = context.Teams
            .Include(team => team.CurrentTask)
            .Include(team => team.Workers)
            .ThenInclude(teamWorker => teamWorker.Worker)
            .ThenInclude(worker => worker.CurrentTodo)
            .OrderBy(team => team.Name)
            .ToList();

        foreach (var team in teams)
        {
            Console.WriteLine($"Team: {team.Name}");
            Console.WriteLine($"\tCurrent task: {team.CurrentTask?.Name ?? "Ingen opgave"}");

            foreach (var teamWorker in team.Workers.OrderBy(teamWorker => teamWorker.Worker.Name))
            {
                Console.WriteLine(
                    $"\tWorker: {teamWorker.Worker.Name} - Current todo: {teamWorker.Worker.CurrentTodo?.Name ?? "Ingen todo"}");
            }
        }
    }

    static void EnsureTeamWithoutTasks(ProjectManagerContext context)
    {
        if (context.Teams.Any(team => team.Name == "Backlog-team"))
        {
            return;
        }

        context.Teams.Add(new Team { Name = "Backlog-team" });
        context.SaveChanges();
    }

    static List<Team> PrintTeamsWithoutTasks()
    {
        using var context = new ProjectManagerContext();

        var teamsWithoutTasks = context.Teams
            .Include(team => team.Tasks)
            .Where(team => !team.Tasks.Any())
            .OrderBy(team => team.Name)
            .ToList();

        Console.WriteLine();
        Console.WriteLine("Teams uden opgaver:");

        foreach (var team in teamsWithoutTasks)
        {
            Console.WriteLine($"\t{team.Name}");
        }

        return teamsWithoutTasks;
    }
}
