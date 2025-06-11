using Kolokwium2Poprawa.Models.DTOs;

namespace Kolokwium2Poprawa.Services;

public interface IDbService
{
    Task<GetCharacterResponseDTO> GetCharacterById(int characterId);
    
    Task<(bool Success, string ErrorMessage)> AddItemsToBackpack(int characterId, List<int> itemIds);
}
