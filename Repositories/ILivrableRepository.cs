using sirius.Entities;
using sirius.Models.Livrable;

namespace sirius.Repositories;

public interface ILivrableRepository
{
    public Task<IEnumerable<Livrable>> GetAllLivrables();
    public Task<Livrable> GetLivrableById(long id);
    public Task<Livrable> CreateLivrable(LivrableCreateDto livrable);
    public Task<Livrable> UpdateLivrable(Livrable livrable);
    public Task<bool> DeleteLivrable(long id);
    
}
