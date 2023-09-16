using Microsoft.AspNetCore.Mvc;
using Accounting.API.DTOs.Account;
using Accounting.API.Services.Account;

namespace Accounting.API.Controllers;

[ApiController]
[Route("v1/Persions/{personID:int}/Accounts")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(200, Type = typeof(AccountsSummaryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<>))]
    public async Task<IActionResult> Index(int personID)
    {
        if (personID <= 0) return BadRequest();

        var res = await _accountService.GetAllAsync(personID);
        return res?.TotalAccounts == -1? NotFound() : Ok(res);
    }

    [HttpGet]
    [Route("{accountID:int}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int personID, int accountID)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        var res = await _accountService.GetAsync(personID, accountID);
        return (res.AccountID == 0 && res.PersonID == 0) ? NotFound() : Ok(res);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(int personID, [FromBody] AccountAddDto account)
    {
        if (personID <= 0) return BadRequest();

        IAccountDto accountDto = account;

        var res = await _accountService.AddAsync(personID, (AccountAddDto)accountDto);

        return res == 0 ? BadRequest() : Created($"v1/Persons/{res}", res);
    }

    [HttpPatch]
    [Route("{accountID:int}/Update")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int personID, int accountID, [FromBody] AccountPatchDto account)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        var res = await _accountService.UpdateAsync(personID, accountID, account);

        return res == 0 ? BadRequest() : NoContent();
    }

    [HttpDelete]
    [Route("{accountID:int}/Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int personID, int accountID)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        var res = await _accountService.DeleteAsync(personID, accountID);
        return res ? NotFound() : Ok();
    }
}
