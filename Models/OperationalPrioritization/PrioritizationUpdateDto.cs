namespace sirius.Models.OperationalPrioritization;

public class PrioritizationUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
}

