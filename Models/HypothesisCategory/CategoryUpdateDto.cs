namespace sirius.Models.HypothesisCategory;

public class CategoryUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
}

