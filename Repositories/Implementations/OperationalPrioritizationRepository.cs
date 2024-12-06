using sirius.Exceptions;
using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Repositories.Implementations;

namespace sirius.Repositories;

public class OperationalPrioritizationRepository : IOperationalPrioritizationRepository
{
     public readonly SiriusDbContext _db;

    public OperationalPrioritizationRepository(SiriusDbContext db)
    {
        _db = db;
    }
    public async Task<List<OperationalPrioritization>> GetAllOperationalPrioritizations()
    {
        return await _db.OperationalPrioritizations.Where(h => h.Active == true).ToListAsync();
    }

    public async Task<OperationalPrioritization> FindOperationalPrioritizationById(long id)
    {
        var prioritization = await _db.OperationalPrioritizations.Where(h => h.Id == id && h.Active == true).FirstOrDefaultAsync();
        return prioritization;
    }

    public async Task<OperationalPrioritization> CreateOperationalPrioritization(OperationalPrioritization prioritization)
    {
        _db.OperationalPrioritizations.Add(prioritization);
        await _db.SaveChangesAsync();
        return prioritization;
    }

    public async Task<OperationalPrioritization> UpdateOperationalPrioritization(OperationalPrioritization prioritization)
    {
        if(!_db.OperationalPrioritizations.Any(at => at.Id == prioritization.Id)) throw new EntityNotFoundException(nameof(OperationalPrioritization), nameof(prioritization.Id), prioritization.Id.ToString());
        _db.OperationalPrioritizations.Update(prioritization);
        await _db.SaveChangesAsync();
        return prioritization;
    }

    public async Task<bool> DeactivateOperationalPrioritization(long id)
    {
        var prioritization = await _db.OperationalPrioritizations.FirstOrDefaultAsync(h => h.Id == id);
        if (prioritization == null) return false;
        
        prioritization.Active = false;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActivateOperationalPrioritization(long id)
    {
        var prioritization = await _db.OperationalPrioritizations.FirstOrDefaultAsync(h => h.Id == id);
        if (prioritization == null) return false;
        
        prioritization.Active = true;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOperationalPrioritization(long id)
    {
        var prioritization = await _db.OperationalPrioritizations.FirstOrDefaultAsync(h => h.Id == id);
        
        if (prioritization == null) return false;
        
        _db.OperationalPrioritizations.Remove(prioritization);
        await _db.SaveChangesAsync();
        return true;
    }
}

