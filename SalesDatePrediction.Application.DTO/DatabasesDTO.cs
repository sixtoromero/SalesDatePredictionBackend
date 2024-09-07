namespace SalesDatePrediction.Application.DTO
{
    public class DatabasesDTO
    {
        public int DatabaseId { get; set; }
        public string? DatabaseName { get; set; }
        public string? ConnectionString { get; set; }
        public string? Description { get; set; }
        public int UsersId { get; set; }
    }
}
