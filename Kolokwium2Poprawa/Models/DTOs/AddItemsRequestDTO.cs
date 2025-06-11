using System.ComponentModel.DataAnnotations;

namespace Kolokwium2Poprawa.Models.DTOs
{
    public class AddItemsRequestDTO
    {
        [Required]
        public List<int> ItemIds { get; set; }
    }
}
