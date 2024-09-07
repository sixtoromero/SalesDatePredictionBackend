namespace SalesDatePrediction.Domain.Entity
{
    public class Products
    {
        public int productid { get; set; }
        public string? productname { get; set; }
        public int supplierid { get; set; }
        public int categoryid { get; set; }
        public decimal unitprice { get; set; }
        public bool discontinued { get; set; }
    }
}
