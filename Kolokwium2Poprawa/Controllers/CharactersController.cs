using Kolokwium2Poprawa.Models.DTOs;
using Kolokwium2Poprawa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2Poprawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CharactersController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            var character = await _dbService.GetCharacterById(characterId);
            
            if (character == null)
            {
                return NotFound($"Character with ID {characterId} not found");
            }
            
            return Ok(character);
        }
        
        [HttpPost("{characterId}/backpacks")]
        public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] List<int> itemIds)
        {
            if (itemIds == null || !itemIds.Any())
            {
                return BadRequest("No items specified to add");
            }
            
            var result = await _dbService.AddItemsToBackpack(characterId, itemIds);
            
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            
            return Ok("Items successfully added to character's backpack");
        }
    }
}
