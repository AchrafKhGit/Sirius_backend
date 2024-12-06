using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sirius.Entities;

public class HypothesisCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required] public string Name { get; set; }
    public bool Active { get; set; }
    
    public List<Hypothesis> Hypothesis { get; set; }
}

