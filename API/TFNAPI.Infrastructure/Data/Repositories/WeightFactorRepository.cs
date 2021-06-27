using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFNAPI.Infrastructure.Data.Context;
using TFNAPI.Infrastructure.Data.Interfaces;

namespace TFNAPI.Infrastructure.Data.Repositories
{
    public class WeightFactorRepository : IWeightingFactorRepository
    {
        private readonly TFNDBContext _tFNDBContext;

        public WeightFactorRepository(TFNDBContext tFNDBContext)
        {
            _tFNDBContext = tFNDBContext;
        }

        public async Task<List<int>> GetWeightingFactorAsync(int digitCount)
        {
            var wFactor =  await _tFNDBContext.WFactors.ToListAsync();
            foreach(var factor in wFactor)
            {
                if(factor.Digits == digitCount)
                {
                    return factor.WeightFactors.Split(",").Select(int.Parse).ToList();
                }
            }
            throw new Exception("Weighting Factor Repo uninitialized");
        }
    }
}
