using AutoMapper;
using InnoGotchi.Application.Common.Exceptions;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Common.Services;

public class PetService : IPetService
{
    private readonly IRepository<Pet> _repository;
    private readonly IMapper _mapper;

    public PetService(IRepository<Pet> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PetViewModel> GetByIdAsync(Guid id)
    {
        var pet = await _repository.GetFirstOrDefaultAsync(
            predicate: p => p.Id == Guid.Empty,
            include: p => p.Include(p => p.Body)
                .ThenInclude(pb => pb.Body)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Eyes)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Nose)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Mouth));

        if (pet is null)
        {
            throw new NotFoundException(nameof(pet), id);
        }

        return _mapper.Map<Pet, PetViewModel>(pet);
    }

    public Task<IList<PetViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PetViewModel> InsertAsync(CreateUpdatePetModel entity)
    {
        var pet = _mapper.Map<CreateUpdatePetModel, Pet>(entity);
        var insertedPetEntry = await _repository.InsertAsync(pet);
        return _mapper.Map<Pet, PetViewModel>(insertedPetEntry.Entity);
    }

    public async Task<PetViewModel> UpdateAsync(Guid id, CreateUpdatePetModel entity)
    {
        var existingPet = await _repository.GetFirstOrDefaultAsync(
            predicate: p => p.Id == Guid.Empty,
            include: p => p.Include(p => p.Body)
                .ThenInclude(pb => pb.Body)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Eyes)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Nose)
                .Include(p => p.Body)
                .ThenInclude(pb => pb.Mouth));
        _mapper.Map<CreateUpdatePetModel, Pet>(entity, existingPet);
        var updatedPet = await _repository.UpdateAsync(existingPet);
        return _mapper.Map<Pet, PetViewModel>(updatedPet.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var pet = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
        await _repository.DeleteAsync(pet);
    }

    public async Task<PetViewModel> FeedPetAsync(Guid id)
    {
        var pet = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
        pet.HungerEnum++;
        var updatedPet = await _repository.UpdateAsync(pet);
        return _mapper.Map<Pet, PetViewModel>(updatedPet.Entity);
    }
    
    public async Task<PetViewModel> HydratePetAsync(Guid id)
    {
        var pet = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
        pet.ThirstEnum++;
        var updatedPet = await _repository.UpdateAsync(pet);
        return _mapper.Map<Pet, PetViewModel>(updatedPet.Entity);
    }
}