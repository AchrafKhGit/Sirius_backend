using sirius.Exceptions;
using Microsoft.EntityFrameworkCore;
using sirius.Enumerations;
using sirius.Configurations;
using sirius.Entities;
using sirius.Models.Hypothesis;
using sirius.Repositories;

namespace sirius.Repositories.Implementations;

public class HypothesisRepository : IHypothesisRepository
{
    private readonly SiriusDbContext _db;

    public HypothesisRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Hypothesis>> GetAllHypothesis()
    {
        return await _db.Hypothesis
            .Include(h => h.Category)
            .ToListAsync();
    }

    public async Task<Hypothesis> GetHypothesisById(long id)
    {
        var hypothesis =  await _db.Hypothesis
            .Include(h => h.Category)
            .FirstOrDefaultAsync(h => h.Id == id);
        if (hypothesis == null) throw new EntityNotFoundException(nameof(Hypothesis), nameof(id), id.ToString());
        return hypothesis;
    }

    public async Task<Hypothesis> CreateHypothesis(HypothesisCreateDto createDto)
    {
        var hypothesis = new Hypothesis
        {
            Nom = createDto.Nom,
            Description = createDto.Description,
            AssociedValue = createDto.AssociedValue,
            AddDate = createDto.AddDate,
            RelatedEntityId = createDto.RelatedEntityId,
            CategoryId = createDto.CategoryId,
            Type = createDto.Type
            
        };
        _db.Hypothesis.Add(hypothesis);
        
        await _db.SaveChangesAsync();
        var result =  await _db.Hypothesis
            .Include(h => h.Category) // Eager load the Category
            .FirstOrDefaultAsync(h => h.Id == hypothesis.Id);
        return result;
    }

    public async Task<Hypothesis> UpdateHypothesis(Hypothesis updateDto)
    {
        var hypothesis = await _db.Hypothesis.AsNoTracking().FirstOrDefaultAsync(h => h.Id == updateDto.Id);
        if (hypothesis == null)
        {
            throw new Exception("Hypothesis not found");
        }
        var previousValue = hypothesis.AssociedValue;
        var newValue = updateDto.AssociedValue;

        var hypothesisHistory = new HypothesisHistory
        {
            PreviousValue = previousValue,
            NewValue = newValue,
            ChangeDate = DateTime.UtcNow,
            HypothesisId = hypothesis.Id
        };

        _db.HypothesisHistories.Add(hypothesisHistory);
        _db.Hypothesis.Attach(updateDto);
        _db.Entry(updateDto).State = EntityState.Modified;
        _db.Entry(hypothesis).CurrentValues.SetValues(updateDto);

        await _db.SaveChangesAsync();
        
        return hypothesis;
    }

    public async Task<bool> DeleteHypothesis(long id)
    {
        var hypothesis = await GetHypothesisById(id);
        if (hypothesis == null)
            return false;

        _db.Hypothesis.Remove(hypothesis);
        await _db.SaveChangesAsync();
        return true;
    }
}