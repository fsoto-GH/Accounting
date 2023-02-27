using Microsoft.AspNetCore.Mvc;
using AccountingAPI.DTOs.Account;
using AccountingAPI.DAOs;
using AccountingAPI.Enums;

namespace AccountingAPI.Controllers;

[ApiController]
[Route("v1")]
public class AccountController : Controller
{
    private readonly IAccountDao _accountService;

    public AccountController(IAccountDao accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Route("Persons/{personID:int}/Accounts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Index(int personID)
    {
        if (personID <= 0) return BadRequest();

        var res = await _accountService.GetAll(personID);
        return res?.TotalAccounts == -1? NotFound() : Ok(res);
    }

    [HttpGet]
    [Route("Persons/{personID:int}/Accounts/{accountID:int}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int personID, int accountID)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        var res = await _accountService.Get(personID, accountID);
        return (res.AccountID == 0 && res.PersonID == 0) ? NotFound() : Ok(res);
    }

    [HttpPost]
    [Route("Persons/{personID:int}/Accounts/Add")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(int personID, [FromBody] AccountAddDto account)
    {
        if (personID <= 0) return BadRequest();

        IAccountDto accountDto = account;
        accountDto.TrimNickName();
        accountDto.TrimType();

        var res = await _accountService.Add(personID, (AccountAddDto)accountDto);

        return res == 0 ? BadRequest() : Created($"v1/Persons/{res}", res);
    }

    [HttpPatch]
    [Route("Persons/{personID:int}/Accounts/{accountID:int}/Update")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int personID, int accountID, [FromBody] AccountPatchDto account)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        IAccountDto accountDto = account;
        accountDto.TrimNickName();
        accountDto.TrimType();

        // validation
        if (!Enum.IsDefined(typeof(AccountType), account?.Type?.ToUpper() ?? string.Empty)) return BadRequest();

        var res = await _accountService.Update(personID, accountID, (AccountPatchDto)accountDto);

        return res == 0 ? BadRequest() : NoContent();
    }

    [HttpDelete]
    [Route("Persons/{personID:int}/Accounts/{accountID:int}/Delete")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int personID, int accountID)
    {
        if (personID <= 0 || accountID <= 0) return BadRequest();

        var res = await _accountService.Delete(personID, accountID);
        return res ? NotFound() : Ok();
    }
}
