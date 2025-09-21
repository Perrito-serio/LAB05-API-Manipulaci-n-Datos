using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_MunozHerrera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ENDPOINT ESPECIAL PARA LA LÓGICA DE NEGOCIO
    [HttpPost("matricular")]
    public async Task<IActionResult> MatricularEstudiante([FromBody] MatriculaRequest request)
    {
        // 1. Validar que el estudiante exista
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(request.IdEstudiante);
        if (estudiante == null)
        {
            return NotFound($"El estudiante con ID {request.IdEstudiante} no existe.");
        }

        // 2. Validar que el curso exista
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(request.IdCurso);
        if (curso == null)
        {
            return NotFound($"El curso con ID {request.IdCurso} no existe.");
        }
        
        // 3. (Opcional) Validar que el estudiante no esté ya matriculado en el mismo curso y semestre
        var yaExiste = await _unitOfWork.Repository<Matricula>().FindAsync(m => 
            m.IdEstudiante == request.IdEstudiante && 
            m.IdCurso == request.IdCurso && 
            m.Semestre == request.Semestre);

        if (yaExiste.Any())
        {
            return BadRequest("El estudiante ya se encuentra matriculado en este curso para el semestre indicado.");
        }

        // 4. Crear la nueva matrícula
        var nuevaMatricula = new Matricula
        {
            IdEstudiante = request.IdEstudiante,
            IdCurso = request.IdCurso,
            Semestre = request.Semestre
        };

        // 5. Agregar la matrícula y guardar los cambios.
        // Si algo falla aquí, Unit of Work se asegura de que no se guarde nada (transacción).
        await _unitOfWork.Repository<Matricula>().AddAsync(nuevaMatricula);
        await _unitOfWork.CompleteAsync();

        return Ok(nuevaMatricula);
    }
    
    // --- Métodos CRUD básicos para la entidad Matrícula ---

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _unitOfWork.Repository<Matricula>().GetAllAsync();
        return Ok(matriculas);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null) return NotFound();
        return Ok(matricula);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null) return NotFound();

        _unitOfWork.Repository<Matricula>().Remove(matricula);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}

// Clase DTO (Data Transfer Object) para recibir los datos de la matrícula
public class MatriculaRequest
{
    public int IdEstudiante { get; set; }
    public int IdCurso { get; set; }
    public string Semestre { get; set; }
}