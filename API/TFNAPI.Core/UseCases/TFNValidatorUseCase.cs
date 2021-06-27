using System;
using System.Threading.Tasks;
using TFNAPI.Core.Interfaces.UseCases;
using TFNAPI.Infrastructure.Data.Interfaces;

namespace TFNAPI.Core.UseCases
{
    public class TFNValidatorUseCase : ITFNValidatorUseCase
    {
        private readonly IWeightingFactorRepository _weightingFactorRepository;

        public TFNValidatorUseCase(IWeightingFactorRepository weightingFactorRepository)
        {
            _weightingFactorRepository = weightingFactorRepository;
        }

        public async Task<bool> Validate(string taxFileNumber)
        {
            var numArray = taxFileNumber.ToCharArray();
            var weightFactor = await _weightingFactorRepository.GetWeightingFactorAsync(taxFileNumber.Length);

            if (weightFactor.Count != taxFileNumber.Length)
            {
                return false;
            }
            int sum = 0;
            for (int i = 0; i < taxFileNumber.Length; i++)
            {
                sum += Convert.ToInt32(numArray[i].ToString()) * weightFactor[i];
            }

            return sum % 11 == 0;
        }

        
    }
}
