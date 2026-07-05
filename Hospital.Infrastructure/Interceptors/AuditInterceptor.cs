using Hospital.Domain.Interfaces;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hospital.Infrastructure.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
        {
            UpdateAuditEntities(eventData.Context);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditEntities(eventData.Context);
        }
        
        return base.SavingChanges(eventData, result);
    }

    #region MetodosPrivados

    private void UpdateAuditEntities(DbContext context)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        Guid userId = _currentUserService.UserId;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.UpdatedDate = null;
                entry.Entity.DeletedDate = null;
                entry.Entity.CreatedBy = userId;
                entry.Entity.RowVersion = Guid.NewGuid();
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = now;
                entry.Entity.UpdatedBy = userId;
                entry.Entity.RowVersion = Guid.NewGuid();
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                
                entry.Entity.DeletedDate = now;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedBy = userId;
            }
        }
    }

    #endregion
}