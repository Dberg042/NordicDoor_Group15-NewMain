using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Models;
using System.Reflection.Emit;

namespace NordicDoor_Group15.Areas.Identity.Data;

public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    //To give access to IHttpContextAccessor for Audit Data with IAuditable
    private readonly IHttpContextAccessor _httpContextAccessor;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.EmployeeNumber).HasMaxLength(10);
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
        }
    }
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options, IHttpContextAccessor httpContextAccessor/*, UserManager<ApplicationUser> userManager*/)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<NordicDoor_Group15.Models.Suggestion> Suggestions { get; set; }
    public DbSet<NordicDoor_Group15.Models.Team> Teams { get; set; }
    public DbSet<NordicDoor_Group15.Models.Membership> Memberships { get; set; }
    public DbSet<NordicDoor_Group15.Models.Photo> Photo { get; set; }
    public DbSet<SuggestionPhoto> SuggestionPhotos { get; set; }
    public DbSet<SuggestionThumbnail> SuggestionThumbnails { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        //Many to Many Membership Primary Key
        builder.Entity<Membership>()
            .HasKey(t => new
            {
                t.TeamID,
                t.UserID
            });

        ////Prevent Cascade Delete from Team to Employee
        ////so we are prevented from deleting a Team with
        ////Employees assigned
        builder.Entity<ApplicationUser>()
            .HasMany<Membership>(d => d.Memberships)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        //Team and Suggestion releation, we are prevented from deleting a suggestion with team assigned
        //builder.Entity<Team>()
        //   .HasMany<Suggestion>(d => d.Suggestions)
        //   .WithOne(p => p.Team)
        //   .HasForeignKey(p => p.TeamID)
        //   .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
          .HasMany<Suggestion>(d => d.Suggestions)
          .WithOne(p => p.Creator)
          .HasForeignKey(p => p.CreatorID)
          .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Photo>()
                .HasOne<Suggestion>(s => s.Suggestion)
                .WithMany(s => s.Photos)
                .HasForeignKey(s => s.SuggestionID)
                .OnDelete(DeleteBehavior.Cascade);
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var userName = GetUserName();

        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditable trackable)
            {
                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedOn = now;
                        trackable.UpdatedBy = userName;
                        break;

                    case EntityState.Added:
                        trackable.CreatedOn = now;
                        //Created By and Updateby  commited, since httpcontext agressor doesnot work properly, when it is fixed, comments should be removed.
                        trackable.CreatedBy = userName;
                        trackable.UpdatedOn = now;
                        trackable.UpdatedBy = userName;
                        break;
                }
            }
        }
    }

    private string GetUserName()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            //We have a HttpContext, but there might not be anyone Authenticated
            return _httpContextAccessor.HttpContext?.User?.Identity.Name ?? "Unknown";
        }
        else
        {
            //No HttpContext so seeding data
            return "Seed Data";
        }
    }
}


