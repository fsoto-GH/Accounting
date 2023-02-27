using AccountingAPI.DAOs;
using AccountingAPI.DTOs.Person;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAPI.Controllers;

[ApiController]
[Route("v1")]
public class PersonController : Controller
{
    private readonly IPersonDao _personService;

    public PersonController(IPersonDao personService)
    {
        _personService = personService;
    }

    [HttpGet]
    [Route("Persons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Index()
    {
        return Ok(await _personService.GetAll());
    }

    [HttpGet]
    [Route("Persons/{personID:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(int personID)
    {
        if (personID <= 0) return BadRequest();

        var res = await _personService.Get(personID);
        return res.PersonID == 0? NotFound() : Ok(res);
    }

    [HttpPost]
    [Route("Persons/Add")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] PersonAddDto person)
    {
        IPersonDto personDto = person;
        personDto.TrimNames();
        var res = await _personService.Add((PersonAddDto)personDto);


        return res == 0 ? BadRequest() : Created($"v1/Persons/{res}", res);
    }

    [HttpPatch]
    [Route("Persons/{personID:int}/Update")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int personID, [FromBody] PersonPatchDto person)
    {
        if (personID <= 0) return BadRequest();

        if (personID <= 0) return BadRequest();
        IPersonDto personDto = person;
        personDto.TrimNames();

        var res = await _personService.Update(personID, (PersonPatchDto)personDto);
        return res == -1 ? BadRequest() : NoContent();
    }

    [HttpDelete]
    [Route("Persons/{personID:int}/Delete")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int personID)
    {
        if (personID <= 0) return BadRequest();

        var res = await _personService.Delete(personID);
        return res ? NotFound() : Ok();
    }
}
