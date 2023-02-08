using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Controllers.Base;

namespace Microsoft.Extensions.DependencyInjection.Controllers;

public sealed class FarmsController : ApiController
{
    private readonly IFarmService _service;

    public FarmsController(IFarmService service)
    {
        _service = service;
    }

    [HttpGet("id")]
    public async Task<ActionResult<FarmViewModel>> GetByIdAsync(Guid id)
    {
        var farmViewModel = await _service.GetByIdAsync(id);
        return Ok(farmViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<IList<FarmViewModel>>> GetAllAsync()
    {
        var farmViewModels = await _service.GetAllAsync();
        return Ok(farmViewModels);
    }

    [HttpPost]
    public async Task<ActionResult<FarmViewModel>> CreateAsync(CreateUpdateFarmModel farm)
    {
        var created = await _service.UpdateAsync(farm);
        return CreatedAtAction("GetByIdAsync", created, new {id = created.Id});
    }

    [HttpPost]
    public async Task<ActionResult<FarmViewModel>> UpdateAsync(CreateUpdateFarmModel farm)
    {
        var updated = await _service.UpdateAsync(farm);
        return Ok(updated);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}