﻿using Accounting.API.Services.Person;
using Accounting.API.DTOs.Person;
using Microsoft.AspNetCore.Mvc;
using Accounting.API.Exceptions.Person;


namespace Accounting.API.Controllers;

[ApiController]
[Route("v1/Persons")]
public class PersonController(IPersonService personService) : Controller
{
    private readonly IPersonService _personService = personService;

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonDto>))]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _personService.GetAllAsync());
    }

    [HttpGet]
    [Route("{personID:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int personID)
    {
        try
        {
            return Ok(await _personService.GetAsync(personID));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] PersonAddDto person)
    {
        try
        {
            return Ok(await _personService.AddAsync(person));
        }
        catch (InvalidPersonAdditionException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch]
    [Route("{personID:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int personID, [FromBody] PersonPatchDto person)
    {
        try
        {
            return Ok(await _personService.UpdateAsync(personID, person));
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidPersonUpdateException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{personID:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int personID)
    {
        try
        {
            await _personService.DeleteAsync(personID);
            return NoContent();
        }
        catch (NotFoundPersonException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidPersonDeletionException e)
        {
            return BadRequest(e.Message);
        }
    }
}
