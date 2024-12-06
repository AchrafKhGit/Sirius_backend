using sirius.Exceptions;
using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Repositories;

namespace sirius.Repositories.Implementations;

public class HypothesisHistoryRepository : IHypothesisHistoryRepository
{
    private readonly SiriusDbContext _db;
    public HypothesisHistoryRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<HypothesisHistory>> GetAllHypothesisHistories()
    {
        return await _db.HypothesisHistories
            .Include(hh => hh.Hypothesis)
            .ToListAsync();
    }

    public async Task<HypothesisHistory> GetHypothesisHistoryById(long id)
    {
        var history = await _db.HypothesisHistories
            .Include(hh => hh.Hypothesis)
            .FirstOrDefaultAsync(hh => hh.Id == id);
        
        if (history == null) throw new EntityNotFoundException(nameof(HypothesisHistory), nameof(id), id.ToString());
        return history;
    }

    public async Task<IEnumerable<HypothesisHistory>> GetHypothesisHistoriesByHypothesisId(long id)
    {
        var histories = await _db.HypothesisHistories
            .Include(hh => hh.Hypothesis)
            .Where(hh => hh.Hypothesis.Id == id)
            .ToListAsync();
        return histories;
    }

    public async Task<HypothesisHistory> CreateHypothesisHistory(HypothesisHistory hypothesisHistory)
    {
        _db.HypothesisHistories.Add(hypothesisHistory);
        await _db.SaveChangesAsync();
        return hypothesisHistory;
    }
    
    

    public async Task<bool> DeleteHypothesisHistory(long id)
    {
        var history = await GetHypothesisHistoryById(id);
        if (history == null)
            return false;

        _db.HypothesisHistories.Remove(history);
        await _db.SaveChangesAsync();
        return true;
    }
}