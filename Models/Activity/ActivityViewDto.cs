namespace sirius.Models.Activity;

public class ActivityViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long Budget { get; set; }
    public string Effort { get; set; }
    public long LotId { get; set; }
    public string LotName { get; set; }
    public List<string> TaskNames { get; set; }
}