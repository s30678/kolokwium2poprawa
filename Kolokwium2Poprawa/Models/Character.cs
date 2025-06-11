using System.ComponentModel.DataAnnotations;

namespace Kolokwium2Poprawa.Models;

public class Character
{
    [Key]
    public int CharacterId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; }

    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    
    public virtual ICollection<Backpack> Backpacks { get; set; } = new List<Backpack>();
    public virtual ICollection<CharacterTitle> CharacterTitles { get; set; } = new List<CharacterTitle>();
}