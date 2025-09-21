using LAB05_MunozHerrera.Models;

namespace LAB05_MunozHerrera.Application.Interfaces;

public interface IEstudianteRepository
{
    Task<IEnumerable<Estudiante>> GetAllAsync();
    Task<Estudiante> GetByIdAsync(int id);
    Task AddAsync(Estudiante estudiante);
    void Update(Estudiante estudiante);
    void Delete(Estudiante estudiante);
}