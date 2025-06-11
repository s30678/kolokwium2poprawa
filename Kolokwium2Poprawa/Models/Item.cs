using System.ComponentModel.DataAnnotations;

namespace Kolokwium2Poprawa.Models;

public class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int Weight { get; set; }
    
    public virtual ICollection<Backpack> Backpacks { get; set; } = new List<Backpack>();
}
