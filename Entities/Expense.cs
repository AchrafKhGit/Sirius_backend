using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sirius.Enumerations;

namespace sirius.Entities;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public ExpenceType Type { get; set; }
    public string Comment { get; set; }
}