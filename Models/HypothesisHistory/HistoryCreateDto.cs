namespace sirius.Models.HypothesisHistory;


public class HistoryCreateDto
{
    public string PreviousValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangeDate { get; set; }
    //public long ChangedById { get; set; }
    public long HypothesisId { get; set; }
}