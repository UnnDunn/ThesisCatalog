using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ThesisCatalog.API.Data.Entities;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Helpers;

public class SetSearchTextInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not ThesisCatalogDbContext dbContext)
            return base.SavingChanges(eventData, result);
        SetSearchText(dbContext);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not ThesisCatalogDbContext dbContext) 
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        SetSearchText(dbContext);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetSearchText(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<CatalogItem>())
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified) continue;
            var catalogItemDataEntity = entry.Entity;
            var catalogItem = (ComputerCatalogItem)catalogItemDataEntity;
            catalogItemDataEntity.SearchText = $"""
                                      "
                                      {catalogItem.CpuDescriptor}
                                      {catalogItem.GpuDescriptor}
                                      {catalogItem.StorageSpecification}
                                      {catalogItem.Memory}
                                      {catalogItem.UsbSpecification}
                                      "
                                      """;
        }
    }
}