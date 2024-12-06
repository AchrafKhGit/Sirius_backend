using sirius.Enumerations;

namespace sirius.Models.Expense;

public class ExpenseUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public ExpenceType Type { get; set; }
    public string Comment { get; set; }
}