using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Models.Objectifs;

namespace sirius.Repositories.Implementations;

public class ObjectifsRepository : IObjectifsRepository
{
    private readonly SiriusDbContext _db;

    public ObjectifsRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Objectifs>> GetAllObjectifs()
    {
        var objectifs = await _db.Objectifs.ToListAsync();
        return objectifs;
    }

    public async Task<Objectifs> GetObjectifById(long id)
    {
        var objectifs = await _db.Objectifs.FirstOrDefaultAsync(x => x.Id == id);
        return objectifs;
    }

    public async Task<Objectifs> CreateObjectif(ObjectifsCreateDto objectifsCreateDto)
    {
        var newObjectifs = new Objectifs()
        {
            Name = objectifsCreateDto.Name,
            Description = objectifsCreateDto.Description,
            Category = objectifsCreateDto.Category,
        };
        _db.Objectifs.Add(newObjectifs);
        await _db.SaveChangesAsync();
        
        return newObjectifs;
    }

    public async Task<Objectifs> UpdateObjectif(Objectifs objectifs)
    {
        var existingObjectifs = await _db.Objectifs.FirstOrDefaultAsync(x => x.Id == objectifs.Id);
        if (existingObjectifs == null) throw new Exception("Objectif not found");
        
        _db.Entry(existingObjectifs).CurrentValues.SetValues(objectifs);
        await _db.SaveChangesAsync();
        
        return existingObjectifs;
    }

    public async Task<bool> DeleteObjectif(long id)
    {
        var existingObjectifs = await _db.Objectifs.FirstOrDefaultAsync(x => x.Id == id);
        if (existingObjectifs == null) return false;
        
        _db.Objectifs.Remove(existingObjectifs);
        await _db.SaveChangesAsync();
        return true;
    }


}