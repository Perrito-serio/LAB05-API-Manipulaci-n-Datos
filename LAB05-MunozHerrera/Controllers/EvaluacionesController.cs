using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // POST: api/evaluaciones/registrar
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarEvaluacion([FromBody] EvaluacionRequest request)
    {
        // 1. Validar que el estudiante y el curso existen
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(request.IdEstudiante);
        if (estudiante == null)
        {
            return NotFound($"El estudiante con ID {request.IdEstudiante} no existe.");
        }

        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(request.IdCurso);
        if (curso == null)
        {
            return NotFound($"El curso con ID {request.IdCurso} no existe.");
        }
        
        // 2. Crear la nueva evaluación
        var nuevaEvaluacion = new Evaluacione
        {
            IdEstudiante = request.IdEstudiante,
            IdCurso = request.IdCurso,
            Calificacion = request.Calificacion,
            Fecha = request.Fecha
        };

        // 3. Guardar en la base de datos
        await _unitOfWork.Repository<Evaluacione>().AddAsync(nuevaEvaluacion);
        await _unitOfWork.CompleteAsync();

        return Ok(nuevaEvaluacion);
    }
    
    // --- Métodos CRUD básicos para la entidad Evaluación ---

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var evaluaciones = await _unitOfWork.Repository<Evaluacione>().GetAllAsync();
        return Ok(evaluaciones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null) return NotFound();
        return Ok(evaluacion);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Evaluacione evaluacion)
    {
        if (id != evaluacion.IdEvaluacion) return BadRequest();

        var existingEvaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (existingEvaluacion == null) return NotFound();
        
        _unitOfWork.Repository<Evaluacione>().Update(evaluacion);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null) return NotFound();

        _unitOfWork.Repository<Evaluacione>().Remove(evaluacion);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}

// DTO para la solicitud de creación de una evaluación
public class EvaluacionRequest
{
    public int IdEstudiante { get; set; }
    public int IdCurso { get; set; }
    public decimal Calificacion { get; set; }
    public DateOnly Fecha { get; set; }
}