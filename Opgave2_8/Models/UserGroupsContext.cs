using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Opgave2_8.Models;

public partial class UserGroupsContext : DbContext
{
    public UserGroupsContext(DbContextOptions<UserGroupsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Name).HasColumnType("VARCHAR(32)");

            entity.HasMany(d => d.Users).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("GroupId", "UserId");
                        j.ToTable("GroupUser");
                        j.IndexerProperty<int>("GroupId").HasColumnType("INT");
                        j.IndexerProperty<int>("UserId").HasColumnType("INT");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Password).HasColumnType("CHAR(36)");
            entity.Property(e => e.Username).HasColumnType("VARCHAR(32)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
