using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Application.Interfaces;
using LAB05_MunozHerrera.Data;
using LAB05_MunozHerrera.Models;

namespace LAB05_MunozHerrera.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    // Los repositorios se inicializan de forma "perezosa" (lazy) la primera vez que se usan
    public IGenericRepository<Estudiante> EstudianteRepository { get; private set; }
    public IGenericRepository<Curso> CursoRepository { get; private set; }
    public IGenericRepository<Matricula> MatriculaRepository { get; private set; }
    // Agrega aquí los demás...

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        // Se instancian los repositorios genéricos pasándoles el contexto
        EstudianteRepository = new GenericRepository<Estudiante>(_context);
        CursoRepository = new GenericRepository<Curso>(_context);
        MatriculaRepository = new GenericRepository<Matricula>(_context);
        // Agrega aquí la inicialización de los demás...
    }

    public async Task<int> CompleteAsync()
    {
        // Aquí es donde se llama a SaveChangesAsync, centralizando la lógica de guardado.
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}