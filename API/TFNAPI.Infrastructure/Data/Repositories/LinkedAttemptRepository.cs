using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFNAPI.Infrastructure.Data.Context;
using TFNAPI.Infrastructure.Data.Entities;
using TFNAPI.Infrastructure.Data.Interfaces.Repositories;

namespace TFNAPI.Infrastructure.Data.Repositories
{
    public class LinkedAttemptRepository : ILinkedAttemptRepository
    {
        private readonly TFNDBContext _tFNDBContext;

        public LinkedAttemptRepository(TFNDBContext tFNDBContext)
        {
            _tFNDBContext = tFNDBContext;
        }

        public async Task AddTfnAsync(string tfnNumber)
        {
            await _tFNDBContext.AddAsync(new Attempt { AttemptTime = DateTime.Now, Tfn = tfnNumber });
            await _tFNDBContext.SaveChangesAsync();
        }

        public async Task<List<Attempt>> GetAttemptsAsync()
        {
           return await _tFNDBContext.Attempts.ToListAsync();
        }

      
    }
}
