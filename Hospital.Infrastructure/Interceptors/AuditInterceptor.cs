using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hospital.Infrastructure.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
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

    private static void UpdateAuditEntities(DbContext context)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.UpdatedDate = null;
                entry.Entity.DeletedDate = null;
                entry.Entity.CreatedBy = Guid.Parse(...);
                entry.Entity.UpdatedBy = Guid.Parse(...);
                entry.Entity.DeletedBy = Guid.Parse(...);
                entry.Entity.RowVersion = Guid.NewGuid();
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = now;
                entry.Entity.DeletedDate = null;
                entry.Entity.UpdatedBy = Guid.Parse(...);
                entry.Entity.RowVersion = Guid.NewGuid();
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.DeletedDate = now;
                entry.Entity.UpdatedDate = now;
                entry.Entity.UpdatedBy = Guid.Parse(...);
            }
        }
    }

    #endregion
}