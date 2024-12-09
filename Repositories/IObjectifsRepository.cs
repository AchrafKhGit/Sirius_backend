using sirius.Entities;
using sirius.Models.Objectifs;

namespace sirius.Repositories;

public interface IObjectifsRepository
{
    public Task<IEnumerable<Objectifs>> GetAllObjectifs();
    public Task<Objectifs> GetObjectifById(long id);
    public Task<Objectifs> CreateObjectif(ObjectifsCreateDto objectifsCreateDto);
    public Task<Objectifs> UpdateObjectif(Objectifs objectifs);
    public Task<bool> DeleteObjectif(long id);
    
}
