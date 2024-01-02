using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Models.Dtos;
using ToDoList.Application.ToDos.Services;
using ToDoList.Domain.Entities;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController(IToDoService toDoService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await toDoService.GetAllAsync();
        
        return result.Any() ? Ok(mapper.Map<IEnumerable<ToDoDto>>(result)) : NoContent();
    }
    
    [HttpGet("{toDoId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid toDoId)
    {
        var result = await toDoService.GetByIdAsync(toDoId);
        
        return result is not null ? Ok(result) : NotFound();
    }
    
    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] ToDoDto todo)
    {
        var result = await toDoService.CreateAsync(mapper.Map<ToDoEntity>(todo));
        
        return Ok(mapper.Map<ToDoDto>(result));
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ToDoDto toDo)
    {
        var foundToDo = await toDoService.GetByIdAsync(toDo.Id, true, HttpContext.RequestAborted);

        if (foundToDo is null)
            return NotFound();

        return Ok(await toDoService.UpdateAsync(mapper.Map(toDo, foundToDo)!, true, HttpContext.RequestAborted));
    }
    
    [HttpDelete("{toDoId:guid}")]
    public async ValueTask<IActionResult> Delete([FromRoute] Guid toDoId)
    {
        await toDoService.DeleteByIdAsync(toDoId);
        
        return Ok();
    }
}