using AutoMapper;
using InnoGotchi.Application.Common.Exceptions;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Common.Services;

public class FarmService : IFarmService
{
    private readonly IRepository<Farm> _repository;
    private readonly IPetService _petService;
    private readonly IMapper _mapper;

    public FarmService(IRepository<Farm> repository, IPetService petService, IMapper mapper)
    {
        _repository = repository;
        _petService = petService;
        _mapper = mapper;
    }
    
    public async Task<FarmViewModel> GetByIdAsync(Guid id)
    {
        var farm = await _repository.GetFirstOrDefaultAsync(
            predicate: f => f.Id == id,
            include: farm => farm
                .Include(f => f.Pets)
                .Include(f => f.Colaborators)
                .Include(f => f.Owner));

        if (farm is null)
        {
            throw new NotFoundException(nameof(farm), id);
        }
        
        return _mapper.Map<Farm, FarmViewModel>(farm);
    }

    public async Task<IList<FarmViewModel>> GetAllAsync()
    {
        var farms = await _repository.GetAllAsync(
            include: farm => farm
                .Include(f => f.Pets)
                .Include(f => f.Colaborators)
                .Include(f => f.Owner));
        
        return _mapper.Map<IList<Farm>, IList<FarmViewModel>>(farms);
    }

    public async Task<FarmViewModel> InsertAsync(CreateUpdateFarmModel entity)
    {
        var farm = _mapper.Map<CreateUpdateFarmModel, Farm>(entity);
        var insertedFarmEntry = await _repository.InsertAsync(farm);
        return _mapper.Map<Farm, FarmViewModel>(insertedFarmEntry.Entity);
    }

    public async Task<FarmViewModel> UpdateAsync(CreateUpdateFarmModel entity)
    {
        var farm = _mapper.Map<CreateUpdateFarmModel, Farm>(entity);
        var updatedFarmEntry = await _repository.UpdateAsync(farm);
        return _mapper.Map<Farm, FarmViewModel>(updatedFarmEntry.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var farm = await _repository.GetFirstOrDefaultAsync(f => f.Id == id);
        await _repository.DeleteAsync(farm);
    }

    public async Task AddPetAsync(CreateUpdatePetModel pet)
    {
        await _petService.InsertAsync(pet);
    }

    public async Task RemovePetAsync(Guid id)
    {
        await _petService.DeleteAsync(id);
    }
}