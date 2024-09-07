namespace SalesDatePrediction.Application.DTO
{
    public class ColumnsDTO
    {
        public int ColumnId { get; set; }
        public int TableId { get; set; }
        public string? ColumnName { get; set; }
        public string? DataType { get; set; }
        public int Size { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string? Description { get; set; }
    }
}
