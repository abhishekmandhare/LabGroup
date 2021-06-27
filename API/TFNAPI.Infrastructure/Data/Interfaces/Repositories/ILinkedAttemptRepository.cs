using System.Collections.Generic;
using System.Threading.Tasks;
using TFNAPI.Infrastructure.Data.Entities;

namespace TFNAPI.Infrastructure.Data.Interfaces.Repositories
{
    public interface ILinkedAttemptRepository
    {
        public Task AddTfnAsync(string tfnNumber);
        Task<List<Attempt>> GetAttemptsAsync();
    }
}
