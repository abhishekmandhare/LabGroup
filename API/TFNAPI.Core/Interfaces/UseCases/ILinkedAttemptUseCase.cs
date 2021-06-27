using System.Threading.Tasks;

namespace TFNAPI.Core.Interfaces.UseCases
{
    public interface ILinkedAttemptUseCase
    {
        public Task<bool> checkLinked(string tfnNumber);
    }
}
