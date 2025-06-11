using Kolokwium2Poprawa.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2Poprawa.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Item> Items { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>().HasData(
            new Character
                { CharacterId = 1, FirstName = "Jan", LastName = "Kowalski", CurrentWeight = 0, MaxWeight = 100 },
            new Character { CharacterId = 2, FirstName = "Jan", LastName = "Nowak", CurrentWeight = 0, MaxWeight = 100 }
        );
        modelBuilder.Entity<Item>().HasData(
            new Item { ItemId = 1, Name = "Sword", Weight = 10 },
            new Item { ItemId = 2, Name = "Shield", Weight = 15 }
        );
        modelBuilder.Entity<Backpack>().HasData(
            new Backpack { BackpackId = 1, CharacterId = 1, ItemId = 1, Amount = 1 },
            new Backpack { BackpackId = 2, CharacterId = 2, ItemId = 2, Amount = 1 }
        );
        modelBuilder.Entity<Title>().HasData(
            new Title { TitleId = 1, Name = "Warrior" },
            new Title { TitleId = 2, Name = "Mage" }
        );
        modelBuilder.Entity<CharacterTitle>().HasData(
            new CharacterTitle
                { CharacterTitleId = 1, CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2023, 1, 1) },
            new CharacterTitle
                { CharacterTitleId = 2, CharacterId = 2, TitleId = 2, AcquiredAt = new DateTime(2023, 1, 1) }
        );
    }
}