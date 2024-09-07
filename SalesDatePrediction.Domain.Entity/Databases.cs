using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Domain.Entity
{
    public class Databases
    {
        public int DatabaseId { get; set; }
        public string? DatabaseName { get; set; }
        public string? ConnectionString { get; set; }
        public string? Description { get; set; }
        public int UsersId { get; set; }
    }
}
