using System.Linq.Expressions;

namespace LAB05_MunozHerrera.Application.Interfaces;

// Usamos <T> para indicar que es una interfaz genérica.
// "where T : class" restringe T para que solo pueda ser una clase.
public interface IGenericRepository<T> where T : class
{
    // Obtener una entidad por su ID
    Task<T> GetByIdAsync(int id);

    // Obtener todas las entidades
    Task<IEnumerable<T>> GetAllAsync();

    // Encontrar entidades basadas en una condición (expresión lambda)
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

    // Agregar una nueva entidad (sin guardar)
    Task AddAsync(T entity);

    // Agregar un rango de entidades (sin guardar)
    Task AddRangeAsync(IEnumerable<T> entities);

    // Marcar una entidad como modificada (sin guardar)
    void Update(T entity);

    // Eliminar una entidad (sin guardar)
    void Remove(T entity);

    // Eliminar un rango de entidades (sin guardar)
    void RemoveRange(IEnumerable<T> entities);
}