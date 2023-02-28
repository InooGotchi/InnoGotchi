using System.Linq.Expressions;
using AutoMapper;
using InnoGotchi.Application.Common.Exceptions;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Models;
using InnoGotchi.Application.Common.Services;
using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace InnoGotchi.Domain.UnitTests;

public class FarmServiceTests
{
    private IFarmService _sut;
    private Mock<IRepository<Farm>> _repoMock;
    private Mock<IPetService> _petService;
    private Mock<IMapper> _mapperMock;
    
    public FarmServiceTests()
    {
        _repoMock = new();
        _mapperMock = new();
        _petService = new();
        _sut = new FarmService(_repoMock.Object, _petService.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task GetByIdAsync_GuidEmpty_ThrowsNotFoundException()
    {
        //Arrange
        Farm farm = null;
        var id = Guid.NewGuid();
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farm));

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.GetByIdAsync(id));
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Farm,bool>>>(),
            It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
            It.Is<bool>(x => x == true)), 
            Times.Once);
    }
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_ThrowsNotFoundException()
    {
        //Arrange
        Farm farm = new();
        FarmViewModel farmViewModel = new();
        var id = Guid.NewGuid();
        
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farm));
        
        _mapperMock.Setup(x => x.Map<Farm, FarmViewModel>(
                It.Is<Farm>(x => x == farm)))
            .Returns(farmViewModel);

        // Act
        var result = await _sut.GetByIdAsync(id);
        
        //Assert
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Farm,bool>>>(),
            It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
            It.Is<bool>(x => x == true)), 
            Times.Once);
        
        _mapperMock.Verify(x => x.Map<Farm, FarmViewModel>(
                It.Is<Farm>(x => x == farm)),
            Times.Once);
        
        Assert.Equivalent(farmViewModel, result);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsIListOfFarms()
    {
        //Arrange
        IList<Farm> farms = new List<Farm>();
        IList<FarmViewModel> farmViewModels = new List<FarmViewModel>();
        
        _repoMock.Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IOrderedQueryable<Farm>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farms));
        
        _mapperMock.Setup(x => x.Map<IList<Farm>, IList<FarmViewModel>>(
                It.Is<IList<Farm>>(x => x == farms)))
            .Returns(farmViewModels);
        
        //Act

        var result = await _sut.GetAllAsync();

        //Assert
        
        _repoMock.Verify(x => x.GetAllAsync(
            It.IsAny<Expression<Func<Farm,bool>>>(),
            It.IsAny<Func<IQueryable<Farm>,IOrderedQueryable<Farm>>>(),
            It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
            It.Is<bool>(x => x == true)),
            Times.Once);
        
        _mapperMock.Verify(x => x.Map<IList<Farm>, IList<FarmViewModel>>(
            It.Is<IList<Farm>>(x => x == farms)), 
            Times.Once);
        
        Assert.Equal(farmViewModels, result);
    }
    
    [Fact]
    public async Task InsertAsync_ValidEntity_ReturnsInsertedModel()
    {
        //Arrange
        CreateUpdateFarmModel incomingModel = new();
        Farm mappedFarm = new();
        Farm insertedEntity = new();
        FarmViewModel mappedInsertResult = new();
        
        _mapperMock.Setup(x => x.Map<CreateUpdateFarmModel, Farm>(
                It.Is<CreateUpdateFarmModel>(x => x == incomingModel)))
            .Returns(mappedFarm);
        
        _repoMock.Setup(x => x.InsertAsync(
                It.Is<Farm>(x => x == mappedFarm),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult(insertedEntity));

        _mapperMock.Setup(x => x.Map<Farm, FarmViewModel>(
                It.Is<Farm>(x => x == insertedEntity)))
            .Returns(mappedInsertResult);
        
        //Act
        var result = await _sut.InsertAsync(incomingModel);
        
        //Assert
        _mapperMock.Verify(x => x.Map<CreateUpdateFarmModel, Farm>(
            It.Is<CreateUpdateFarmModel>(x => x == incomingModel)), Times.Once());
        
        _repoMock.Verify(x => x.InsertAsync(
            It.Is<Farm>(x => x == mappedFarm),
            It.IsAny<CancellationToken>()), Times.Once);
        
        _mapperMock.Verify(x => x.Map<Farm, FarmViewModel>(
            It.Is<Farm>(x => x == insertedEntity)), Times.Once());
        
        Assert.Equivalent(mappedInsertResult, result);
    }
    
    [Fact]
    public async Task InsertAsync_NullEntity_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.InsertAsync(null));
    }

    [Fact]
    public async Task UpdateAsync_ValidEntity_ReturnsUpdatedModel()
    {
        CreateUpdateFarmModel incomingModel = new();
        Farm existingFarm = new();
        Farm updatedFarm = new();
        FarmViewModel mappedUpdateResult = new();
        var id = Guid.NewGuid();

        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Farm, bool>>>(),
            It.IsAny<Func<IQueryable<Farm>, IIncludableQueryable<Farm, object>>>(),
            It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(existingFarm));

        _mapperMock.Setup(x => x.Map<CreateUpdateFarmModel, Farm>(
            It.Is<CreateUpdateFarmModel>(x => x == incomingModel),
            It.Is<Farm>(x => x == existingFarm)));

        _repoMock.Setup(x => x.UpdateAsync(
            It.Is<Farm>(x => x == existingFarm),
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(updatedFarm));
        
        _mapperMock.Setup(x => x.Map<Farm, FarmViewModel>(
            It.Is<Farm>(x => x == updatedFarm)))
            .Returns(mappedUpdateResult);
        
        // Act

        var result = await _sut.UpdateAsync(id, incomingModel);
        
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Farm, bool>>>(),
            It.IsAny<Func<IQueryable<Farm>, IIncludableQueryable<Farm, object>>>(),
            It.Is<bool>(x => x == true)),
            Times.Once);
        
        _mapperMock.Verify(x => x.Map<CreateUpdateFarmModel, Farm>(
            It.Is<CreateUpdateFarmModel>(x => x == incomingModel),
            It.Is<Farm>(x => x == existingFarm)),
            Times.Once);
        
        _repoMock.Verify(x => x.UpdateAsync(
            It.Is<Farm>(x => x == existingFarm),
            It.IsAny<CancellationToken>()),
            Times.Once);
        
        _mapperMock.Verify(x => x.Map<Farm, FarmViewModel>(
            It.Is<Farm>(x => x == updatedFarm)),
            Times.Once);
        
        Assert.Equivalent(mappedUpdateResult, result);
    }    
    
    [Fact]
    public async Task UpdateAsync_NullEntity_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.UpdateAsync(Guid.NewGuid(), null));
    }

    [Fact]
    public async Task UpdateAsync_EntityWithProvidedIdDoesNotExists_ThrowsNotFoundException()
    {
        CreateUpdateFarmModel farmModel = new();
        Farm farm = null;
        var id = Guid.NewGuid();
        
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farm));
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.UpdateAsync(id, farmModel));
        
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)), 
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_EntityWithProvidedIdDoesNotExists_ThrowsNotFoundException()
    {
        Farm farm = null;
        var id = Guid.NewGuid();
        
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farm));
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.DeleteAsync(id));
        
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)), 
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_EntityExists_DeletesEntity()
    {
        Farm farm = new();
        var id = Guid.NewGuid();
        
        _repoMock.Setup(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)))
            .Returns(Task.FromResult(farm));

        _repoMock.Setup(x => x.DeleteAsync(
            It.Is<Farm>(x => x == farm),
            It.IsAny<CancellationToken>()));

        await _sut.DeleteAsync(id);
        
        _repoMock.Verify(x => x.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Farm,bool>>>(),
                It.IsAny<Func<IQueryable<Farm>,IIncludableQueryable<Farm,object>>>(),
                It.Is<bool>(x => x == true)), 
            Times.Once);
        
        _repoMock.Verify(x => x.DeleteAsync(
            It.Is<Farm>(x => x == farm),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task AddPetAsync_CallsPetServiceMethod()
    {
        CreateUpdatePetModel incomingModel = new();
        _petService.Setup(x => x.InsertAsync(incomingModel));

        await _sut.AddPetAsync(incomingModel);
        
        _petService.Verify(x => x.InsertAsync(incomingModel),
            Times.Once);
    }
    
    [Fact]
    public async Task RemovePetAsync_CallsPetServiceMethod()
    {
        var id = Guid.NewGuid();

        _petService.Setup(x => x.DeleteAsync(id));
        
        await _sut.RemovePetAsync(id);
        
        _petService.Verify(x => x.DeleteAsync(id),
            Times.Once);
    }
}