namespace sirius.Models.Activity;

public class ActivityCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long Budget { get; set; }
    public string Effort { get; set; }
    public long LotId { get; set; }
}