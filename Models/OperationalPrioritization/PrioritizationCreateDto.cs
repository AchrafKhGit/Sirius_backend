using System.ComponentModel.DataAnnotations;

namespace sirius.Models.OperationalPrioritization;

public class PrioritizationCreateDto
{
    [Required] public string Name { get; set; }
    public bool Active { get; set; } = true;
}