using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Transaction;
using Accounting.API.Services.Transaction;
using Accounting.API.DTOs.Transaction;
using Microsoft.AspNetCore.Mvc;
using Accounting.API.Controllers.QueryParamaters;

namespace Accounting.API.Controllers
{
    [ApiController]
    [Route("v1/Persons/{personID:int}/Accounts/{accountID:int}/Transactions")]
    public class TransactionController(ITransactionService transactionService) : Controller
    {
        private readonly ITransactionService _transactionService = transactionService;

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync(int personID, int accountID, [FromQuery]TransactionQueryParameters queryParameters)
        {
            try
            {
                return Ok(await _transactionService.GetAllAsync(personID, accountID, queryParameters));
            }
            catch (NotFoundPersonException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundAccountException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("{transactionID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int personID, int accountID, int transactionID)
        {
            try
            {
                return Ok(await _transactionService.GetAsync(personID, accountID, transactionID));
            }
            catch (NotFoundPersonException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundAccountException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundTransactionException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync(int personID, int accountID, TransactionAddDto transactionAddDto)
        {
            try
            {
                return Ok(await _transactionService.AddAsync(personID, accountID, transactionAddDto));
            }
            catch (NotFoundPersonException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundAccountException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidTransactionAdditionException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("{transactionID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transactionPatchDto)
        {
            try
            {
                return Ok(await _transactionService.UpdateAsync(personID, accountID, transactionID, transactionPatchDto));
            }
            catch (NotFoundPersonException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundAccountException e)
            {
                return NotFound(e.Message);
            }
            catch (NotFoundTransactionException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidAccountAdditionException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidTransactionUpdateException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
