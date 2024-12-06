namespace sirius.Models.HypothesisHistory;

public class HistoryUpdateDto
{
    public long Id { get; set; }
    public string PreviousValue { get; set; }
    public string NewValue { get; set; }
    public DateTime Date { get; set; }
    public long HypothesisId { get; set; }
}

