using sirius.Entities;

namespace sirius.Repositories.Implementations;

public interface IHypothesisCategoryRepository
{
    public Task<List<HypothesisCategory>> GetAllHypothesisCategory();
    public Task<HypothesisCategory> FindHypothesisCategoryById(long id);
    public Task<HypothesisCategory> CreateHypothesisCategory(HypothesisCategory hypothesisCategory);
    public Task<HypothesisCategory> UpdateHypothesisCategory(HypothesisCategory hypothesisCategory);
    public Task<bool> DeactivateHypothesisCategory(long categoryId);
    public Task<bool> ActivateHypothesisCategory(long categoryId);
    public Task<bool> DeleteHypothesisCategory(long categoryId);
}

