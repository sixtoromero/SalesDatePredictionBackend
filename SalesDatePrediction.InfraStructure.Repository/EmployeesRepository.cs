using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public EmployeesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspGetEmployees";

                var result = await connection.QueryAsync<Employees>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }        

        public Task<Employees> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Employees model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Employees model)
        {
            throw new NotImplementedException();
        }
    }
}
