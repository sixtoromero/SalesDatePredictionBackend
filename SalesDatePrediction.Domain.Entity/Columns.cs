namespace SalesDatePrediction.Domain.Entity
{
    public class Columns
    {
        public int ColumnId { get; set; }
        public int TableId { get; set; }
        public string? ColumnName { get; set; }
        public string? DataType { get; set; }
        public string? Size { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string? Description { get; set; }
        public int UsersId { get; set; }
    }
}
