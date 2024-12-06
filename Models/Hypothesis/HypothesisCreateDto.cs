using sirius.Enumerations;

namespace sirius.Models.Hypothesis;

public class HypothesisCreateDto
{
    public string Nom { get; set; }
    public string Description { get; set; }
    public string AssociedValue { get; set; }
    public DateTime AddDate { get; set; } = DateTime.UtcNow;
    public string RelatedEntityId { get; set; }
    public long CategoryId { get; set; }
    public HypothesisType Type { get; set; }
}

