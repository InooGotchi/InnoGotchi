using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Domain.Common;
using InnoGotchi.Domain.Common.BodyParts;
using InnoGotchi.Infrastructure.Identity;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InnoGotchi.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) :
        base(options, operationalStoreOptions)
    {
    }

    public virtual DbSet<Farm> Farms { get; set; }
    public virtual DbSet<Pet> Pets { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<PetBody> PetBodies { get; set; }
    public virtual DbSet<Nose> Noses { get; set; }
    public virtual DbSet<Eyes> Eyes { get; set; }
    public virtual DbSet<Mouth> Mouths { get; set; }
    public virtual DbSet<Body> Bodies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Farm>()
            .HasMany(f => f.Pets)
            .WithOne(p => p.Farm)
            .HasForeignKey(p => p.Id);
        
        builder.Entity<Farm>()
            .HasOne(f => f.Owner)
            .WithMany(o => o.Farms)
            .HasForeignKey(f => f.Id);

        builder.Entity<User>()
            .HasMany(u => u.Farms)
            .WithOne(f => f.Owner)
            .HasForeignKey(f => f.Id);

        builder.Entity<Pet>()
            .HasOne(p => p.Farm)
            .WithMany(f => f.Pets)
            .HasForeignKey(p => p.Id);

        builder.Entity<Pet>()
            .HasOne(p => p.Body)
            .WithOne(pb => pb.Pet);

        builder.Entity<PetBody>()
            .HasOne(pb => pb.Body)
            .WithMany(b => b.PetBodies)
            .HasForeignKey(pb => pb.BodyId);
        
        builder.Entity<PetBody>()
            .HasOne(pb => pb.Eyes)
            .WithMany(b => b.PetBodies)
            .HasForeignKey(pb => pb.EyesId);
        
        builder.Entity<PetBody>()
            .HasOne(pb => pb.Mouth)
            .WithMany(b => b.PetBodies)
            .HasForeignKey(pb => pb.MouthId);

        builder.Entity<PetBody>()
            .HasOne(pb => pb.Nose)
            .WithMany(b => b.PetBodies)
            .HasForeignKey(pb => pb.NoseId);

        base.OnModelCreating(builder);
    }
}
