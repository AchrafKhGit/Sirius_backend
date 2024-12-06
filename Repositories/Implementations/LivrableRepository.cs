using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Models.Livrable;

namespace sirius.Repositories.Implementations;

public class LivrableRepository : ILivrableRepository
{
    private readonly SiriusDbContext _db;

    public LivrableRepository(SiriusDbContext db)
    {
        _db = db;
    }
    
    
    public async Task<IEnumerable<Livrable>> GetAllLivrables()
    {
        var livrables = await _db.Livrables.ToListAsync();
        return livrables;
    }

    public async Task<Livrable> GetLivrableById(long id)
    {
        var livrable = await _db.Livrables.FirstOrDefaultAsync(x => x.Id == id);
        return livrable;
    }

    public async Task<Livrable> CreateLivrable(LivrableCreateDto livrable)
    {
        var newLivrable = new Livrable()
        {
            Name = livrable.Name,
            Description = livrable.Description,
            Nature = livrable.Nature,
        };
        _db.Livrables.Add(newLivrable);
        await _db.SaveChangesAsync();
        
        return newLivrable;
    }

    public async Task<Livrable> UpdateLivrable(Livrable livrable)
    {
        var existingLivrable = await _db.Livrables.FirstOrDefaultAsync(x => x.Id == livrable.Id);
        if (existingLivrable == null) throw new Exception("Livrable not found");
        
        _db.Entry(existingLivrable).CurrentValues.SetValues(livrable);
        await _db.SaveChangesAsync();
        
        return livrable;
    }

    public async Task<bool> DeleteLivrable(long id)
    {
        var existingLivrable = await _db.Livrables.FirstOrDefaultAsync(x => x.Id == id);
        if (existingLivrable == null) return false;
        
        _db.Livrables.Remove(existingLivrable);
        await _db.SaveChangesAsync();
        return true;
    }
}