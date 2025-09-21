using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesoresController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _unitOfWork.Repository<Profesore>().GetAllAsync();
        return Ok(profesores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesore = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesore == null) return NotFound();
        return Ok(profesore);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Profesore profesore)
    {
        await _unitOfWork.Repository<Profesore>().AddAsync(profesore);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetById), new { id = profesore.IdProfesor }, profesore);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Profesore profesore)
    {
        if (id != profesore.IdProfesor) return BadRequest();

        var existingProfesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (existingProfesor == null) return NotFound();

        _unitOfWork.Repository<Profesore>().Update(profesore);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var profesore = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesore == null) return NotFound();

        _unitOfWork.Repository<Profesore>().Remove(profesore);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}