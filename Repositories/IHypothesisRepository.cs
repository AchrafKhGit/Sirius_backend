using sirius.Entities;
using sirius.Models.Hypothesis;

namespace sirius.Repositories;

public interface IHypothesisRepository
{
    Task<IEnumerable<Hypothesis>> GetAllHypothesis();
    Task<Hypothesis> GetHypothesisById(long id);
    Task<Hypothesis> CreateHypothesis(HypothesisCreateDto hypothesis);
    Task<Hypothesis> UpdateHypothesis(Hypothesis hypothesis);
    Task<bool> DeleteHypothesis(long id);
}