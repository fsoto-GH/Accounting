using Accounting.API.Services.Person;
using Accounting.API.DTOs.Person;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("v1/Persons")]
public class PersonController : Controller
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Index()
    {
        var res = await _personService.GetAllAsync();

        return Ok(res);
    }

    [HttpGet]
    [Route("{personID:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int personID)
    {
        if (personID <= 0)
            return BadRequest();

        var res = await _personService.GetAsync(personID);

        if (res is null)
            return NotFound();

        return Ok(res);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] PersonAddDto person)
    {
        var res = await _personService.AddAsync(person);

        if (res is null)
            return BadRequest();

        return Ok(res);
    }

    [HttpPatch]
    [Route("{personID:int}/Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int personID, [FromBody] PersonPatchDto person)
    {
        if (personID <= 0)
            return BadRequest();

        var res = await _personService.UpdateAsync(personID, person);

        if (res is null)
            return NotFound();

        return Ok(res);
    }

    [HttpDelete]
    [Route("{personID:int}/Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int personID)
    {
        if (personID <= 0)
            return BadRequest();

        var didDelete = await _personService.DeleteAsync(personID);
        return didDelete ? NoContent(): NotFound();
    }
}
