using sirius.Entities;

namespace sirius.Repositories.Implementations;

public interface IOperationalPrioritizationRepository
{
    public Task<List<OperationalPrioritization>> GetAllOperationalPrioritizations();
    public Task<OperationalPrioritization> FindOperationalPrioritizationById(long id);
    public Task<OperationalPrioritization> CreateOperationalPrioritization(OperationalPrioritization prioritization);
    public Task<OperationalPrioritization> UpdateOperationalPrioritization(OperationalPrioritization prioritization);
    public Task<bool> DeactivateOperationalPrioritization(long id);
    public Task<bool> ActivateOperationalPrioritization(long id);
    public Task<bool> DeleteOperationalPrioritization(long id);
}