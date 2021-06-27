using System;
using System.Threading.Tasks;
using TFNAPI.Core.Interfaces.UseCases;
using TFNAPI.Infrastructure.Data.Interfaces.Repositories;

namespace TFNAPI.Core.UseCases
{
    public class LinkedAttemptsUseCase : ILinkedAttemptUseCase
    {
        private readonly ILinkedAttemptRepository _linkedAttemptRepository;

        public LinkedAttemptsUseCase(ILinkedAttemptRepository linkedAttemptRepository)
        {
            _linkedAttemptRepository = linkedAttemptRepository;
        }
       
        public async Task<bool> checkLinked(string tfnNumber)
        {
            bool isLinked = await IsLinkedAsync(tfnNumber);
            await _linkedAttemptRepository.AddTfnAsync(tfnNumber);
            return isLinked;
        }

        public async Task<bool> IsLinkedAsync(string tfnNumber)
        {
            var attemptedTFN = await _linkedAttemptRepository.GetAttemptsAsync();
            int previousAttemptsCount = attemptedTFN.Count;
            int linkedCount = 1;
            string firstNumber = tfnNumber;
            for (int i = previousAttemptsCount - 1; i >= 0; i--)
            {
                if (IsStaleDate(attemptedTFN[i].AttemptTime))
                {
                    break;
                }
                if (AreNumbersLinked(firstNumber, attemptedTFN[i].Tfn))
                {
                    firstNumber = attemptedTFN[i].Tfn;
                    linkedCount++;
                    if (linkedCount == 3)
                    {
                        break;
                    }
                }
            }
            
            return linkedCount >= 3;

        }

        public bool AreNumbersLinked(string number1, string number2)
        {
            for (int i = 0; i < (number1.Length - 4); i++)
            {
                var subString = number1.Substring(i, 4);
                if (number2.Contains(subString))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStaleDate(DateTime date)
        {
            return date.AddSeconds(30) < DateTime.Now;
        }
    }
}
