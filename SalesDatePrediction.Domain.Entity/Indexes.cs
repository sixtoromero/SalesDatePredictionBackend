namespace SalesDatePrediction.Domain.Entity
{
    public class Indexes
    {
        public int IndexId { get; set; }
        public int TableId { get; set; }
        public string? IndexName { get; set; }
        public string? IndexType { get; set; }
        public string? IncludedColumns { get; set; }
        public int UsersId { get; set; }
    }
}
