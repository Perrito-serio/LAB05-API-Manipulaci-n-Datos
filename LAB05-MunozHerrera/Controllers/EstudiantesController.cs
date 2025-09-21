using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    // Inyectamos IUnitOfWork, que nos da acceso a todos los repositorios
    public EstudiantesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/estudiantes
    [HttpGet]
    public async Task<IActionResult> GetEstudiantes()
    {
        var estudiantes = await _unitOfWork.EstudianteRepository.GetAllAsync();
        return Ok(estudiantes);
    }

    // POST: api/estudiantes
    [HttpPost]
    public async Task<IActionResult> CreateEstudiante(Estudiante estudiante)
    {
        // 1. Agregamos la entidad usando el repositorio
        await _unitOfWork.EstudianteRepository.AddAsync(estudiante);

        // 2. Guardamos TODOS los cambios pendientes en la base de datos
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction("GetEstudiante", new { id = estudiante.IdEstudiante }, estudiante);
    }
}