using sirius.Entities;
using sirius.Models.Expense;

namespace sirius.Repositories;

public interface IExpenseRepository
{
    public Task<IEnumerable<Expense>> GetAllExpenses();
    public Task<Expense> GetExpenseById(long id);
    public Task<Expense> CreateExpense(ExpenseCreateDto expense);
    public Task<Expense> UpdateExpense(Expense expense);
    public Task<bool> DeleteExpense(long id);
    
}