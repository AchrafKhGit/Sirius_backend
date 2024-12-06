namespace sirius.Models.Expense;

public class ExpenseViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string TypeName { get; set; }
    public string Comment { get; set; }
}