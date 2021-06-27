using System.Threading.Tasks;

namespace TFNAPI.Core.Interfaces.UseCases
{
    public interface ITFNValidatorUseCase
    {
        public Task<bool> Validate(string taxFileNumber);
    }
}
