using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudiantesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/estudiantes
    [HttpGet]
    public async Task<IActionResult> GetEstudiantes()
    {
        // ANTES: _unitOfWork.EstudianteRepository.GetAllAsync()
        // AHORA:
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        return Ok(estudiantes);
    }

    // POST: api/estudiantes
    [HttpPost]
    public async Task<IActionResult> CreateEstudiante(Estudiante estudiante)
    {
        // ANTES: _unitOfWork.EstudianteRepository.AddAsync(estudiante)
        // AHORA:
        await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);

        // El guardado se mantiene igual
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction("GetEstudiante", new { id = estudiante.IdEstudiante }, estudiante);
    }
    
    // Y así para los demás métodos...
}