using LAB05_MunozHerrera.Application.Interfaces;
using LAB05_MunozHerrera.Models;

namespace LAB05_MunozHerrera.Application;

public interface IUnitOfWork : IDisposable
{
    // Una propiedad por cada repositorio que necesites
    IGenericRepository<Estudiante> EstudianteRepository { get; }
    IGenericRepository<Curso> CursoRepository { get; }
    IGenericRepository<Matricula> MatriculaRepository { get; }
    // Agrega aquí los demás repositorios...

    // El método que guardará todos los cambios en la base de datos
    Task<int> CompleteAsync();
}