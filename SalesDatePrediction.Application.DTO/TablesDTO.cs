using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.DTO
{
    public class TablesDTO
    {
        public int TableId { get; set; }
        public string? Scheme { get; set; }
        public string? TableName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
