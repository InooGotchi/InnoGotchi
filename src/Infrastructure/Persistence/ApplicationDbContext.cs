using System.Reflection;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Domain.Common;
using InnoGotchi.Domain.Common.Base;
using InnoGotchi.Domain.Common.BodyParts;
using InnoGotchi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) :
        base(options)
    {
        _currentUserService = currentUserService;
    }

    public virtual DbSet<Pet> Pets { get; set; }
    public virtual DbSet<Farm> Farms { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<PetBody> PetBodies { get; set; }
    public virtual DbSet<Nose> Noses { get; set; }
    public virtual DbSet<Eyes> Eyes { get; set; }
    public virtual DbSet<Mouth> Mouths { get; set; }
    public virtual DbSet<Body> Bodies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && x.State == EntityState.Added);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedBy = _currentUserService.UserId;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}