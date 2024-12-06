using System.ComponentModel.DataAnnotations;

namespace sirius.Entities;

public class MigrationHistory
{
    [Key]
    public string MigrationId { get; set; }
    public DateTime AppliedOn { get; set; }
}