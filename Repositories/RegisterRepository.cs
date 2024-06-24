
using contactForm.Entities;
using contactForm.Helper;
using Dapper;

namespace contactForm.Repositories
{
    public interface IRegisterRepository
    {
        Task<IEnumerable<RegisterEntities>> GetAll();
        Task<RegisterEntities> GetById(int id);
        Task Create(RegisterEntities registerData);
        Task Update(RegisterEntities registerData);
        Task Delete(int id);
    }

    public class RegisterRepository : IRegisterRepository
    {

        private readonly DataContext _context;

        public RegisterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(RegisterEntities registerData)
        {
            using (var connection = _context.CreateConnection())
            {
                Guid id = Guid.NewGuid();
                var sql = $"INSERT INTO register (id, first_name, last_name, email, message, query_type) VALUES('{id}', '{registerData.FirstName}', '{registerData.LastName}', '{registerData.EmailAddress}', '{registerData.Message}', '{registerData.QueryType}')";
                await connection.ExecuteAsync(sql, registerData);
            };
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegisterEntities>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM register";
            return await connection.QueryAsync<RegisterEntities>(sql);
        }

        public async Task<RegisterEntities> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM register WHERE Id = {id}";
            return await connection.QuerySingleOrDefaultAsync<RegisterEntities>(sql, new { id });
        }

        public Task Update(RegisterEntities registerData)
        {
            throw new NotImplementedException();
        }
    }
}