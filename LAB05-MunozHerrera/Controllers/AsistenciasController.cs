using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // POST: api/asistencias/registrar
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarAsistencia([FromBody] AsistenciaRequest request)
    {
        // Validaciones
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(request.IdEstudiante);
        if (estudiante == null) return NotFound($"Estudiante con ID {request.IdEstudiante} no encontrado.");

        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(request.IdCurso);
        if (curso == null) return NotFound($"Curso con ID {request.IdCurso} no encontrado.");
        
        var nuevaAsistencia = new Asistencia
        {
            IdEstudiante = request.IdEstudiante,
            IdCurso = request.IdCurso,
            Fecha = request.Fecha,
            Estado = request.Estado
        };

        await _unitOfWork.Repository<Asistencia>().AddAsync(nuevaAsistencia);
        await _unitOfWork.CompleteAsync();

        return Ok(nuevaAsistencia);
    }
    
    // --- Métodos CRUD básicos para la entidad Asistencia ---

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var asistencias = await _unitOfWork.Repository<Asistencia>().GetAllAsync();
        return Ok(asistencias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null) return NotFound();
        return Ok(asistencia);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Asistencia asistencia)
    {
        if (id != asistencia.IdAsistencia) return BadRequest();

        var existingAsistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (existingAsistencia == null) return NotFound();

        _unitOfWork.Repository<Asistencia>().Update(asistencia);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null) return NotFound();

        _unitOfWork.Repository<Asistencia>().Remove(asistencia);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}

// DTO para la solicitud de creación de una asistencia
public class AsistenciaRequest
{
    public int IdEstudiante { get; set; }
    public int IdCurso { get; set; }
    public DateOnly Fecha { get; set; }
    public string Estado { get; set; } // "Presente", "Ausente", "Justificada"
}