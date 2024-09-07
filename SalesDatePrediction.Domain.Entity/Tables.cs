namespace SalesDatePrediction.Domain.Entity
{
    public class Tables
    {
        public int TableId { get; set; }
        public int DatabaseId { get; set; }
        public string? Scheme { get; set; }
        public string? TableName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int UsersId { get; set; }
    }
}
