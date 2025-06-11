using Kolokwium2Poprawa.Data;
using Kolokwium2Poprawa.Models;
using Kolokwium2Poprawa.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2Poprawa.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<GetCharacterResponseDTO> GetCharacterById(int characterId)
    {
        var character = await _context.Characters
            .Where(c => c.CharacterId == characterId)
            .FirstOrDefaultAsync();

        if (character == null)
        {
            return null;
        }
        
        var backpackItems = await _context.Backpacks
            .Where(b => b.CharacterId == characterId)
            .Join(_context.Items,
                b => b.ItemId,
                i => i.ItemId,
                (b, i) => new BackpackItemDTO
                {
                    ItemName = i.Name,
                    ItemWeight = i.Weight,
                    Amount = b.Amount
                })
            .ToListAsync();
        
        var characterTitles = await _context.CharacterTitles
            .Where(ct => ct.CharacterId == characterId)
            .Join(_context.Titles,
                ct => ct.TitleId,
                t => t.TitleId,
                (ct, t) => new CharacterTitleDTO
                {
                    Title = t.Name,
                    AquiredAt = ct.AcquiredAt
                })
            .ToListAsync();

        return new GetCharacterResponseDTO
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = backpackItems,
            Titles = characterTitles
        };
    }
    
    public async Task<(bool Success, string ErrorMessage)> AddItemsToBackpack(int characterId, List<int> itemIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.CharacterId == characterId);
            
            if (character == null)
            {
                return (false, $"Character with ID {characterId} not found");
            }
            
            var items = await _context.Items
                .Where(i => itemIds.Contains(i.ItemId))
                .ToListAsync();
            
            if (items.Count != itemIds.Count)
            {
                return (false, "One or more items do not exist");
            }
            
            int totalNewWeight = items.Sum(i => i.Weight);
            
            int availableCapacity = character.MaxWeight - character.CurrentWeight;
            
            if (totalNewWeight > availableCapacity)
            {
                return (false, "Character doesn't have enough carrying capacity");
            }
            
            foreach (var item in items)
            {
                var existingBackpackItem = await _context.Backpacks
                    .FirstOrDefaultAsync(b => b.CharacterId == characterId && b.ItemId == item.ItemId);
                
                if (existingBackpackItem != null)
                {
                    existingBackpackItem.Amount++;
                }
                else
                {
                    await _context.Backpacks.AddAsync(new Backpack
                    {
                        CharacterId = characterId,
                        ItemId = item.ItemId,
                        Amount = 1
                    });
                }
            }
            
            character.CurrentWeight += totalNewWeight;
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return (true, null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"An error occurred while adding items: {ex.Message}");
        }
    }
}
