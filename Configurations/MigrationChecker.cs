using sirius.Entities;

namespace sirius.Configurations;

public static class MigrationChecker
{
    public static bool HasMigrationBeenApplied(SiriusDbContext context, string migrationId)
    {
        return context.MigrationHistories.Any(m => m.MigrationId == migrationId);
    }

    public static void MarkMigrationAsApplied(SiriusDbContext context, string migrationId)
    {
        var migrationHistory = new MigrationHistory
        {
            MigrationId = migrationId,
            AppliedOn = DateTime.UtcNow
        };
        context.MigrationHistories.Add(migrationHistory);
        context.SaveChanges();
    }
}

