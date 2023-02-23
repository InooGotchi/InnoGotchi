using System.Linq.Expressions;
using AutoMapper;
using InnoGotchi.Application.Common.Exceptions;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using InnoGotchi.Application.Common.Services;
using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace InnoGotchi.Domain.UnitTests;

public class PetService_Tests
{
    protected IPetService _sut;
    private Mock<IRepository<Pet>> _repoMock;
    private Mock<IMapper> _mapperMock;
    
    public PetService_Tests()
    {
        _repoMock = new();
        _mapperMock = new();
        _sut = new PetService(
            _repoMock.Object,
            _mapperMock.Object);
    }
    
    [Fact]
    public async Task  GetByIdAsync_GuidEmpty_ThrowsNotFoundException()
    {
        //Arrange
        Pet pet = null;
        var id = new Guid();
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Pet,bool>>>(),
                It.IsAny<Func<IQueryable<Pet>,IIncludableQueryable<Pet,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(pet));
        
        //Assert
        Assert.ThrowsAsync<NotFoundException>(() => _sut.GetByIdAsync(id));
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Pet,bool>>>(),
            It.IsAny<Func<IQueryable<Pet>,IIncludableQueryable<Pet,object>>>(),
            It.Is<bool>(x => x == true)), Times.Once);
    }
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsValidPet()
    {
        //Arrange
        Pet pet = new();
        PetViewModel mappingResult = new();
        var id = Guid.NewGuid();
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Pet,bool>>>(),
                It.IsAny<Func<IQueryable<Pet>,IIncludableQueryable<Pet,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(pet));
        _mapperMock.Setup(x => x.Map<Pet, PetViewModel>(It.Is<Pet>(x => x == pet)))
            .Returns(mappingResult);
        //Act
        var result = await _sut.GetByIdAsync(id);
        
        //Assert
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Pet,bool>>>(),
            It.IsAny<Func<IQueryable<Pet>,IIncludableQueryable<Pet,object>>>(),
            It.Is<bool>(x => x == true)), Times.Once);
        _mapperMock.Verify(x => x.Map<Pet, PetViewModel>(It.Is<Pet>(x => x == pet)), Times.Once);
        Assert.Equivalent(mappingResult, result);
    }
    
    [Fact]
    public async Task GetAllAsync_ThrowsNotImplementedException()
    {
        //Arrange
        
        //Act
        
        //Assert
        Assert.ThrowsAsync<NotImplementedException>( () => _sut.GetAllAsync());
    }
    
    [Fact]
    public async Task InsertAsync_ValidEntity_ReturnsInsertedModel()
    {
        //Arrange
        CreateUpdatePetModel petModel = new();
        Pet mappingResult = new();
        PetViewModel mappedInsertResult = new();
        _mapperMock.Setup(x => x.Map<CreateUpdatePetModel, Pet>(
            It.Is<CreateUpdatePetModel>(x => x == petModel)))
            .Returns(mappingResult);
        _repoMock.Setup(x => x.InsertAsync(
                It.Is<Pet>(x => x == mappingResult),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult<Pet>(mappingResult));
        _mapperMock.Setup(x => x.Map<Pet, PetViewModel>(It.Is<Pet>(x => x == mappingResult)))
            .Returns(mappedInsertResult);
        //Act
        var result = await _sut.InsertAsync(petModel);
        
        //Assert
        _mapperMock.Verify(x => x.Map<CreateUpdatePetModel, Pet>(
            It.Is<CreateUpdatePetModel>(x => x == petModel)), Times.Once());
        _mapperMock.Verify(x => x.Map<Pet, PetViewModel>(
            It.Is<Pet>(x => x == mappingResult)), Times.Once());
        _repoMock.Verify(x => x.InsertAsync(
            It.Is<Pet>(x => x == mappingResult),
            It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equivalent(mappedInsertResult, result);
    }
    
    [Fact]
    public async Task InsertAsync_NullEntity_ThrowsArgumentNullException()
    {
        //Arrange
        CreateUpdatePetModel argument = new();
        Pet mappingResult = new();
        
        _mapperMock.Setup(x => x.Map<CreateUpdatePetModel, Pet>(
            It.Is<CreateUpdatePetModel>(x => x == argument)))
            .Returns(mappingResult);
        _repoMock.Setup(x => x.InsertAsync(
                It.Is<Pet>(x => x == mappingResult),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult<Pet>(mappingResult));
        
        //Act
        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _sut.InsertAsync(argument));
        _mapperMock.Verify(x => x.Map<CreateUpdatePetModel, Pet>(
            It.Is<CreateUpdatePetModel>(x => x == argument)), Times.Once());
        _mapperMock.Verify(x => x.Map<Pet, PetViewModel>(
            It.Is<Pet>(x => x == mappingResult)), Times.Once());
    }
        
    [Fact]
    public async Task UpdateAsync_ValidEntity_ReturnsUpdatedEntity()
    {
        //Arrange
        Pet existingPet = new();
        CreateUpdatePetModel argument = new();
        Pet mappedExistingPet = new();
        
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Pet,bool>>>(),
                It.IsAny<Func<IQueryable<Pet>,IIncludableQueryable<Pet,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(existingPet));

        _mapperMock.Setup(x => x.Map<CreateUpdatePetModel, Pet>(
                It.Is<CreateUpdatePetModel>(x => x == argument),
                It.Is<Pet>(x => x == existingPet)))
            .Returns(mappedExistingPet);
        
        _repoMock.Setup(x => x.UpdateAsync(
                It.Is<CreateUpdatePetModel>(x => x == ),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult<Pet>());
        
        //Act
        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _sut.InsertAsync(argument));
        _mapperMock.Verify(x => x.Map<CreateUpdatePetModel, Pet>(
            It.Is<CreateUpdatePetModel>(x => x == argument)), Times.Once());
        _mapperMock.Verify(x => x.Map<Pet, PetViewModel>(
            It.Is<Pet>(x => x == )), Times.Once());
    }
    
    
}