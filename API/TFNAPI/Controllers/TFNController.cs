using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using TFNAPI.Core.Interfaces.UseCases;
using TFNAPI.Model;

namespace TFNAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TFNController : ControllerBase
    {
        private readonly ILinkedAttemptUseCase _linkedAttemptUseCase;
        private readonly ITFNValidatorUseCase _tFNValidatorUseCase;
        private readonly ILogger<TFNController> _logger;

        public TFNController(ILinkedAttemptUseCase linkedAttemptUseCase, ITFNValidatorUseCase tFNValidatorUseCase, ILogger<TFNController> logger)
        {
            _linkedAttemptUseCase = linkedAttemptUseCase;
            _tFNValidatorUseCase = tFNValidatorUseCase;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateTFN(TaxFileNumber taxFileNumber)
        {
            try
            {
                string errorMessage = "Tax File Number is valid.";
                bool isError = false;
                bool isLinked = await _linkedAttemptUseCase.checkLinked(taxFileNumber.TFN);
                if (isLinked)
                {
                    _logger.LogInformation($"Multiple attempts detected. Latest TFN : {taxFileNumber.TFN}");
                    errorMessage = "Validation tool does not allow multiple attempts within a give time frame.";
                    isError = isLinked;
                }
                else
                {
                    bool isValid = await _tFNValidatorUseCase.Validate(taxFileNumber.TFN);
                    if (!isValid)
                    {
                        _logger.LogInformation($"TFN is invalid : {taxFileNumber}");
                        isError = true;
                        errorMessage = "Tax File Number is invalid.";
                    }
                }
                _logger.LogInformation("Successfully serviced Post Request");
                return Ok(new { TfnIsValid = !isError, ErrorMessage = errorMessage });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
