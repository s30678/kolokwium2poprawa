using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2Poprawa.Models;

[Table("Character_Title")]
public class CharacterTitle
{
    [Key]
    public int CharacterTitleId { get; set; }

    [ForeignKey("Character")]
    public int CharacterId { get; set; }

    [ForeignKey("Title")]
    public int TitleId { get; set; }

    public DateTime AcquiredAt { get; set; }


    public virtual Character Character { get; set; }
    public virtual Title Title { get; set; }
}