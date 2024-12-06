using sirius.Entities;
namespace sirius.Models.Hypothesis;


public class HypothesisViewDto
{
    public long Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public string AssociedValue { get; set; }
    public DateTime AddDate { get; set; }
    public string RelatedEntityId { get; set; }
    public long HypothesisCategoryId { get; set; }
    public string HypothesisCategoryName { get; set; }
    public string HypothesisTypeName { get; set; }
    public List<Entities.HypothesisHistory> HypothesisHistories { get; set; }
}

