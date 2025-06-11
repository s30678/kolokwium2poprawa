namespace Kolokwium2Poprawa.Models.DTOs;

public class GetCharacterResponseDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemDTO> BackpackItems { get; set; }
    public List<CharacterTitleDTO> Titles { get; set; }
}

public class BackpackItemDTO
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class CharacterTitleDTO
{
    public string Title { get; set; }
    public DateTime AquiredAt { get; set; }
}
