using sirius.Enumerations;

namespace sirius.Models.Hypothesis;


public class HypothesisUpdateDto
{
    public long Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public string AssociedValue { get; set; }
    public DateTime AddDate { get; set; }
    public string RelatedEntityId { get; set; }
    public long HypothesisCategoryId { get; set; }
    public HypothesisType HypothesisType { get; set; }
}

