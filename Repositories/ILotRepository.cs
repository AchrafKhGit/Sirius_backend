using sirius.Entities;
using sirius.Models.Lot;

namespace Siruis_backend.Repositories.Implementations;

public interface ILotRepository
{
    public Task<IEnumerable<Lot>> GetAllLots();
    public Task<Lot> GetLotById(long id);
    public Task<Lot> GetLotByName(string name);
    public Task<IEnumerable<Activity>> GetActivitiesByLotId(long lotId);
    public Task<Lot> CreateLot(LotCreateDto createDto);
    public Task<Lot> UpdateLot(Lot lot);
    public Task<bool> DeleteLot(long id);
}