namespace sirius.Entities;

public class HypothesisHistory
{
    public long Id { get; set; }
    public string PreviousValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangeDate { get; set; }

    // Relationship
    public long HypothesisId { get; set; }
    public Hypothesis Hypothesis { get; set; }

    //public long ChangedById { get; set; }
    //public User ChangedBy { get; set; }
}

