using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CursosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _unitOfWork.Repository<Curso>().GetAllAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null) return NotFound();
        return Ok(curso);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Curso curso)
    {
        await _unitOfWork.Repository<Curso>().AddAsync(curso);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Curso curso)
    {
        if (id != curso.IdCurso) return BadRequest();
        
        var existingCurso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (existingCurso == null) return NotFound();

        _unitOfWork.Repository<Curso>().Update(curso);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null) return NotFound();

        _unitOfWork.Repository<Curso>().Remove(curso);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}