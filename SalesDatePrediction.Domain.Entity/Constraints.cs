namespace SalesDatePrediction.Domain.Entity
{
    public class Constraints
    {
        public int ConstraintId { get; set; }
        public int TableId { get; set; }
        public string? ConstraintName { get; set; }
        public string? ConstraintType { get; set; }
        public string? Description { get; set; }
        public int UsersId { get; set; }
    }
}
