using sirius.Models.Hypothesis;

namespace sirius.Models.HypothesisCategory;

public class CategoryViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public List<HypothesisViewDto> Hypothesis { get; set; }
}

