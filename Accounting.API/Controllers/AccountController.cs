using Microsoft.AspNetCore.Mvc;
using Accounting.API.DTOs.Account;
using Accounting.API.Services.Account;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.Controllers.QueryParamaters;

namespace Accounting.API.Controllers;

[ApiController]
[Route("v1/Persons/{personID:int}/Accounts")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountsSummaryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(int personID)
    {
        try
        {
            return Ok(await _accountService.GetAllAsync(personID));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    [Route("{accountID:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int personID, int accountID)
    {
        try
        {
            return Ok(await _accountService.GetAsync(personID, accountID));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        } catch (NotFoundAccountException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddAsync(int personID, [FromBody] AccountAddDto account)
    {
        try
        {
            return Ok(await _accountService.AddAsync(personID, account));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e);
        }
    }

    [HttpPatch]
    [Route("{accountID:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(int personID, int accountID, [FromBody] AccountPatchDto account)
    {
        try
        {
            return Ok(await _accountService.UpdateAsync(personID, accountID, account));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        }
        catch (NotFoundAccountException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidAccountUpdateException e)
        {
            return BadRequest(e.Message);
        }
    }
}
