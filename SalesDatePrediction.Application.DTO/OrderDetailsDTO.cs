namespace SalesDatePrediction.Application.DTO
{
    public class OrderDetailsDTO
    {
        public int orderid { get; set; }
        public int productid { get; set; }
        public decimal unitprice { get; set; }
        public short qty { get; set; }
        public decimal discount { get; set; }
    }
}
