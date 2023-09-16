using Accounting.API.Services.Transaction;
using Accounting.API.DTOs.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("Persons/{personID:int}/Accounts/{accountID:int}/Transactions/Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAsync(int personID, int accountID, TransactionAddDto transactionAddDto)
        {
            var res = await _transactionService.AddAsync(personID, accountID, transactionAddDto);

            if (res is null)
                return BadRequest();

            return Ok(res);
        }

        [HttpPatch]
        [Route("Persons/{personID:int}/Accounts/{accountID:int}/Transactions/Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transactionPatchDto)
        {
            var res = await _transactionService.UpdateAsync(personID, accountID, transactionID, transactionPatchDto);

            if (res is null)
                return BadRequest();

            return Ok(res);
        }

        [HttpDelete]
        [Route("Persons/{personID:int}/Accounts/{accountID:int}/Transactions/{transactionID:int}/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int personID, int accountID, int transactionID)
        {
            var res = await _transactionService.DeleteAsync(personID, accountID, transactionID);
            return res ? NoContent() : BadRequest();
        }

        [HttpGet]
        [Route("Persons/{personID:int}/Accounts/{accountID:int}/Transactions/{transactionID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int personID, int accountID, int transactionID)
        {
            var res = await _transactionService.GetAsync(personID, accountID, transactionID);
            return res is null ? NotFound() : Ok(res);
        }

        [HttpGet]
        [Route("Persons/{personID:int}/Accounts/{accountID:int}/Transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync(int personID, int accountID)
        {
            var res = await _transactionService.GetAllAsync(personID, accountID);

            return res.AccountID == 0 && res.PersonID == 0 ? NotFound() : Ok(res);
        }
    }
}
