using System.Data;

namespace SalesDatePrediction.Transversal.Common
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
        IDbConnection GetConnectionWithString(string connectionString);
    }
}
