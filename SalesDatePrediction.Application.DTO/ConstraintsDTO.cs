using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.DTO
{
    public class ConstraintsDTO
    {
        public int ConstraintId { get; set; }
        public int TableId { get; set; }
        public string? ConstraintName { get; set; }
        public string? ConstraintType { get; set; }
        public string? Description { get; set; }
    }
}
