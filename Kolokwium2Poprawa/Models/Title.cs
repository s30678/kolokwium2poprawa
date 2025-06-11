using System.ComponentModel.DataAnnotations;

namespace Kolokwium2Poprawa.Models;

public class Title
{
    [Key]
    public int TitleId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public virtual ICollection<CharacterTitle> CharacterTitles { get; set; }
}