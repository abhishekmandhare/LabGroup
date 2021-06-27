using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFNAPI.Infrastructure.Data.Interfaces
{
    public interface IWeightingFactorRepository
    {
        public Task<List<int>> GetWeightingFactorAsync(int digitCount);
    }
}
