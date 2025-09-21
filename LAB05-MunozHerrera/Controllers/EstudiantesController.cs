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
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        return Ok(estudiantes);
    }

    // GET: api/estudiantes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound(); // Retorna 404 si no se encuentra
        }
        return Ok(estudiante);
    }

    // POST: api/estudiantes
    [HttpPost]
    public async Task<IActionResult> Create(Estudiante estudiante)
    {
        await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
        await _unitOfWork.CompleteAsync();
        
        // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
    }
    
    // PUT: api/estudiantes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Estudiante estudiante)
    {
        if (id != estudiante.IdEstudiante)
        {
            return BadRequest("El ID del estudiante no coincide.");
        }

        var existingEstudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (existingEstudiante == null)
        {
            return NotFound();
        }

        // El repositorio genérico se encarga de marcar la entidad como modificada.
        _unitOfWork.Repository<Estudiante>().Update(estudiante);
        await _unitOfWork.CompleteAsync();

        return NoContent(); // Retorna 204 No Content, que es estándar para una actualización exitosa.
    }

    // DELETE: api/estudiantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Estudiante>().Remove(estudiante);
        await _unitOfWork.CompleteAsync();

        return NoContent(); // Retorna 204 No Content, estándar para una eliminación exitosa.
    }
}