using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Controllers.Base;

namespace Microsoft.Extensions.DependencyInjection.Controllers;

public sealed class PetsController : ApiController
{
    private readonly IPetService _service;

    public PetsController(IPetService service)
    {
        _service = service;
    }

    [HttpGet("id")]
    public async Task<ActionResult<PetViewModel>> GetPetByIdAsync(Guid id)
    {
        var petViewModel = await _service.GetByIdAsync(id);
        return Ok(petViewModel);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<PetViewModel>> UpdatePetAsync(Guid id, CreateUpdatePetModel petModel)
    {
        var updatedPet = await _service.UpdateAsync(id, petModel);
        return Ok(updatedPet);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<PetViewModel>> DeletePetAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPost("id/feed")]
    public async Task<ActionResult<PetViewModel>> FeedPetAsync(Guid id)
    {
        var updatedPet = await _service.FeedPetAsync(id);
        return Ok(updatedPet);
    }
    
    [HttpPost("id/hydrate")]
    public async Task<ActionResult<PetViewModel>> HydratePetAsync(Guid id)
    {
        var updatedPet = await _service.HydratePetAsync(id);
        return Ok(updatedPet);
    }
}