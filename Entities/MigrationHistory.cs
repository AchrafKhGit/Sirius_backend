using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sirius.Entities;

public class MigrationHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string MigrationId { get; set; }
    public DateTime AppliedOn { get; set; }
}