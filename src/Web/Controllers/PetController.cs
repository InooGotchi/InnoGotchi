using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Controllers.Base;

namespace Microsoft.Extensions.DependencyInjection.Controllers;

public sealed class PetController : ApiController
{
    private readonly IPetService _service;

    public PetController(IPetService service) => _service = service;

    [HttpGet("{id}")]
    public async Task<ActionResult<PetViewModel>> GetPetByIdAsync(Guid id)
    {
        var petViewModel = await _service.GetByIdAsync(id);
        return Ok(petViewModel);
    }

    [HttpPost("Update")]
    public async Task<ActionResult<PetViewModel>> UpdatePetAsync(CreateUpdatePetModel petModel)
    {
        var updatedPet = await _service.UpdateAsync(petModel);
        return Ok(updatedPet);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PetViewModel>> DeletePetAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("Feed/{id}")]
    public async Task<ActionResult<PetViewModel>> FeedPetAsync(Guid id)
    {
        var updatedPet = await _service.FeedPetAsync(id);
        return Ok(updatedPet);
    }

    [HttpPut("Drink/{id}")]
    public async Task<ActionResult<PetViewModel>> DrinkPetAsync(Guid id)
    {
        var updatedPet = await _service.DrinkPetAsync(id);
        return Ok(updatedPet);
    }
}