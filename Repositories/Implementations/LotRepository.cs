using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Models.Activity;
using sirius.Models.Lot;
using Siruis_backend.Repositories.Implementations;

namespace sirius.Repositories.Implementations;

public class LotRepository : ILotRepository
{
    private readonly SiriusDbContext _db;

    public LotRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Lot>> GetAllLots()
    {
        var lots = await _db.Lots.ToListAsync();
        return lots;
    }

    public async Task<Lot> GetLotById(long id)
    {
        var lot = await _db.Lots.FirstOrDefaultAsync(x => x.Id == id);
        return lot;
    }

    public async Task<Lot> GetLotByName(string name)
    {
        var lot = await _db.Lots.FirstOrDefaultAsync(x => x.Name == name);
        return lot;
    }

    public async Task<IEnumerable<Entities.Activity>> GetActivitiesByLotId(long id)
    {
        var activities = await _db.Activities.Where(x => x.LotId == id).ToListAsync();
        return activities;
    }

    public async Task<Lot> CreateLot(LotCreateDto createDto)
    {
        var lot = new Lot()
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Budget = createDto.Budget,
            Effort = createDto.Effort,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
        };
        
        await _db.Lots.AddAsync(lot);
        await _db.SaveChangesAsync();
        return lot;
    }

    public async Task<Lot> UpdateLot(Lot lot)
    {
        var existingLot = await _db.Lots.FirstOrDefaultAsync(x => x.Id == lot.Id);
        if (existingLot == null)
            throw new Exception("Lot not found");
        
        _db.Entry(existingLot).CurrentValues.SetValues(lot);
        await _db.SaveChangesAsync();
        
        return existingLot;
    }

    public async Task<bool> DeleteLot(long id)
    {
        var existingLot = await _db.Lots.FirstOrDefaultAsync(x => x.Id == id);
        if (existingLot == null) return false;
        
        _db.Lots.Remove(existingLot);
        await _db.SaveChangesAsync();
        return true;
    }
}