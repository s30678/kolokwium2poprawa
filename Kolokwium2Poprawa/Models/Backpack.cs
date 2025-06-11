using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2Poprawa.Models;

public class Backpack
{
    [Key]
    public int BackpackId { get; set; }

    [ForeignKey("Character")]
    public int CharacterId { get; set; }

    [ForeignKey("Item")]
    public int ItemId { get; set; }

    public int Amount { get; set; }
    
    public virtual Character Character { get; set; }
    public virtual Item Item { get; set; }
}