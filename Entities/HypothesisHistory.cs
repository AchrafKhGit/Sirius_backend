using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sirius.Entities;

public class HypothesisHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string PreviousValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangeDate { get; set; }

    // Relationship
    public long HypothesisId { get; set; }
    public Hypothesis Hypothesis { get; set; }

    //public long ChangedById { get; set; }
    //public User ChangedBy { get; set; }
}

