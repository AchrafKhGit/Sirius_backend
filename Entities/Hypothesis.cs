using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sirius.Enumerations;

namespace sirius.Entities;

public class Hypothesis
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public string AssociedValue { get; set; }
    public DateTime AddDate { get; set; }
    public string RelatedEntityId { get; set; }

    // Relationships
    public long CategoryId { get; set; }
    public HypothesisCategory Category { get; set; }
    public HypothesisType Type { get; set; }
    public List<HypothesisHistory>? History { get; set; }
}