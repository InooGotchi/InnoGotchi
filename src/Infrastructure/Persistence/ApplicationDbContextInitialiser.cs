using InnoGotchi.Domain.Common;
using InnoGotchi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InnoGotchi.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
                await SeedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        // Default users
        var user = new ApplicationUser { UserName = "user@localhost", Email = "user@localhost" };

        if (_userManager.Users.All(u => u.UserName != user.UserName))
        {
            await _userManager.CreateAsync(user, "User111222'");
        }
        user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);


        // Default data
        // Seed, if necessary

        if (!_context.Farms.Any())
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                CreatedBy = user.Id,
                ApplicationUserId = user.Id,
                ImagePath = "Seeded",
            };

            var farm = new Farm()
            {
                Id = Guid.NewGuid(),
                CreatedBy = player.ApplicationUserId,
                Name = "SeedFarm",
                Capacity = 4,
                TotalPets = 2,
                AlivePets = 2,
                OwnerId = player.Id,
            };

            var pets = new List<Pet>()
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = player.ApplicationUserId,
                    Name = "SeededPet",
                    Age = 0,
                    NextDrinkDate = DateTime.UtcNow.AddDays(5),
                    NextFeedDate = DateTime.UtcNow.AddDays(5),
                    FarmId = farm.Id
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = player.ApplicationUserId,
                    Name = "SeededPet2",
                    Age = 0,
                    NextDrinkDate = DateTime.UtcNow.AddDays(5),
                    NextFeedDate = DateTime.UtcNow.AddDays(5),
                    FarmId = farm.Id
                },
            };

            _context.Farms.Add(farm);
            _context.Players.Add(player);
            _context.Pets.AddRange(pets);

            _context.SaveChanges();
        }
    }
}
