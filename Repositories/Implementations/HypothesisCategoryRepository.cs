

using sirius.Exceptions;
using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Repositories.Implementations;

namespace sirius.Repositories.Implementations;

public class HypothesisCategoryRepository : IHypothesisCategoryRepository
{
    public readonly SiriusDbContext _db;

    public HypothesisCategoryRepository(SiriusDbContext db)
    {
        _db = db;
    }
    public async Task<List<HypothesisCategory>> GetAllHypothesisCategory()
    {
        return await _db.HypothesisCategories.Where(h => h.Active == true).ToListAsync();
    }

    public async Task<HypothesisCategory> FindHypothesisCategoryById(long id)
    {
        var category = await _db.HypothesisCategories.Where(h => h.Id == id && h.Active == true).FirstOrDefaultAsync();
        return category;
    }

    public async Task<HypothesisCategory> CreateHypothesisCategory(HypothesisCategory hypothesisCategory)
    {
        _db.HypothesisCategories.Add(hypothesisCategory);
        await _db.SaveChangesAsync();
        return hypothesisCategory;
    }

    public async Task<HypothesisCategory> UpdateHypothesisCategory(HypothesisCategory hypothesisCategory)
    {
        if(!_db.HypothesisCategories.Any(at => at.Id == hypothesisCategory.Id)) throw new EntityNotFoundException(nameof(HypothesisCategory), nameof(hypothesisCategory.Id), hypothesisCategory.Id.ToString());
        _db.HypothesisCategories.Update(hypothesisCategory);
        await _db.SaveChangesAsync();
        return hypothesisCategory;
    }

    public async Task<bool> DeactivateHypothesisCategory(long categoryId)
    {
        var category = await _db.HypothesisCategories.FirstOrDefaultAsync(h => h.Id == categoryId);
        if (category == null) return false;
        
        category.Active = false;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActivateHypothesisCategory(long categoryId)
    {
        var category = await _db.HypothesisCategories.FirstOrDefaultAsync(h => h.Id == categoryId);
        if (category == null) return false;
        
        category.Active = true;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteHypothesisCategory(long categoryId)
    {
        var category = await _db.HypothesisCategories.FirstOrDefaultAsync(h => h.Id == categoryId);
        
        if (category == null) return false;
        
        _db.HypothesisCategories.Remove(category);
        await _db.SaveChangesAsync();
        return true;
    }
}

