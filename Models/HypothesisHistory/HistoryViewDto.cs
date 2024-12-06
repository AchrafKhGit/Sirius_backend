namespace sirius.Models.HypothesisHistory;


public class HistoryViewDto
{
    public long Id { get; set; }
    public string PreviousValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangeDate { get; set; }
    //public long ChangedById { get; set; }
    //public string ChangedByName { get; set; }
    public long HypothesisId { get; set; }
    public string HypothesisName { get; set; }
    public string HypothesisDescription { get; set; }
    public string HypothesisType { get; set; }
}

