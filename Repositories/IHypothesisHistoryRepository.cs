using sirius.Entities;

namespace sirius.Repositories;

public interface IHypothesisHistoryRepository
{
    Task<IEnumerable<HypothesisHistory>> GetAllHypothesisHistories();
    Task<HypothesisHistory> GetHypothesisHistoryById(long id);
    Task<IEnumerable<HypothesisHistory>> GetHypothesisHistoriesByHypothesisId(long id);
    Task<HypothesisHistory> CreateHypothesisHistory(HypothesisHistory hypothesisHistory);
    Task<bool> DeleteHypothesisHistory(long id);
}