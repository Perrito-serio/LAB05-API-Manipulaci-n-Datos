using System.Collections;
using LAB05_MunozHerrera.Application;
using LAB05_MunozHerrera.Application.Interfaces;
using LAB05_MunozHerrera.Data;

namespace LAB05_MunozHerrera.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    // Usaremos una Hashtable para almacenar las instancias de los repositorios que vayamos creando
    private Hashtable _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null)
        {
            // Si la tabla hash no existe, la creamos
            _repositories = new Hashtable();
        }

        // Obtenemos el nombre del tipo de la entidad (ej. "Estudiante")
        var type = typeof(T).Name;

        // Verificamos si ya hemos creado un repositorio para este tipo
        if (!_repositories.ContainsKey(type))
        {
            // Si no existe, creamos una nueva instancia del repositorio genérico
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            
            // Y la agregamos a nuestra tabla hash
            _repositories.Add(type, repositoryInstance);
        }

        // Devolvemos el repositorio (casteado al tipo correcto)
        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}