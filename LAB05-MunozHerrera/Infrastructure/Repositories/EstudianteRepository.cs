using LAB05_MunozHerrera.Application.Interfaces;
using LAB05_MunozHerrera.Data;
using LAB05_MunozHerrera.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB05_MunozHerrera.Infrastructure.Repositories;

public class EstudianteRepository : IEstudianteRepository
{
    private readonly AppDbContext _context;

    public EstudianteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Estudiante>> GetAllAsync()
    {
        return await _context.Estudiantes.ToListAsync();
    }

    public async Task<Estudiante> GetByIdAsync(int id)
    {
        return await _context.Estudiantes.FindAsync(id);
    }

    public async Task AddAsync(Estudiante estudiante)
    {
        await _context.Estudiantes.AddAsync(estudiante);
    }

    public void Update(Estudiante estudiante)
    {
        _context.Estudiantes.Update(estudiante);
    }

    public void Delete(Estudiante estudiante)
    {
        _context.Estudiantes.Remove(estudiante);
    }
}