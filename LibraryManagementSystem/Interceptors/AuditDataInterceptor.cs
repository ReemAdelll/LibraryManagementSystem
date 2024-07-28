using LibraryManagementSystem.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibraryManagementSystem.Interceptors
{
    public class AuditDataInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return base.SavingChanges(eventData, result);
            }
            var ChangeTracker = eventData.Context.ChangeTracker;

            var AddedList = ChangeTracker.Entries<BaseEntity<int>>().Where(a => a.State == EntityState.Added).ToList();
           foreach(var item in AddedList)
            {
                item.Property(a=>a.CreationTime).CurrentValue = DateTime.UtcNow;
            }

            var ModifiedList = ChangeTracker.Entries<BaseEntity<int>>().Where(a => a.State == EntityState.Modified).ToList();
            foreach(var item in ModifiedList)
            {
                item.Property(a=>a.LastUpdateTime).CurrentValue = DateTime.UtcNow;
            }
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            var ChangeTracker = eventData.Context.ChangeTracker;

            var AddedList = ChangeTracker.Entries<BaseEntity<int>>().Where(a => a.State == EntityState.Added).ToList();
            foreach (var item in AddedList)
            {
                item.Property(a => a.CreationTime).CurrentValue = DateTime.UtcNow;
            }

            var ModifiedList = ChangeTracker.Entries<BaseEntity<int>>().Where(a => a.State == EntityState.Modified).ToList();
            foreach (var item in ModifiedList)
            {
                item.Property(a => a.LastUpdateTime).CurrentValue = DateTime.UtcNow;
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken); 
        }
    }
}
