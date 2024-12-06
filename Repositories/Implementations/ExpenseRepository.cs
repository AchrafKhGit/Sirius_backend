using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using sirius.Models.Expense;

namespace sirius.Repositories.Implementations;

public class ExpenseRepository : IExpenseRepository
{
    private readonly SiriusDbContext _db;

    public ExpenseRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Expense>> GetAllExpenses()
    {
        return await _db.Expenses.ToListAsync();
    }

    public async Task<Expense> GetExpenseById(long id)
    {
        return await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Expense> CreateExpense(ExpenseCreateDto createDto)
    {
        var expense = new Expense()
        {
            Name = createDto.Name,
            Value = createDto.Value,
            Type = createDto.Type,
            Comment = createDto.Comment,
        };
        
        await _db.Expenses.AddAsync(expense);
        await _db.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> UpdateExpense(Expense expense)
    {
        var existingExpense = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == expense.Id);
        if (existingExpense == null) throw new Exception(" Expense not found");
        
        _db.Entry(existingExpense).CurrentValues.SetValues(expense);
        await _db.SaveChangesAsync();
        return existingExpense;
    }

    public async Task<bool> DeleteExpense(long id)
    {
        var existingExpense = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        if (existingExpense == null) return false;
        
        _db.Expenses.Remove(existingExpense);
        await _db.SaveChangesAsync();
        return true;
    }
}