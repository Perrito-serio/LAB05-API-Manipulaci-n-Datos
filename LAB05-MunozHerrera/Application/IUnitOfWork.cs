using LAB05_MunozHerrera.Application.Interfaces;

namespace LAB05_MunozHerrera.Application;

public interface IUnitOfWork : IDisposable
{
    // MÉTODO GENÉRICO PARA OBTENER CUALQUIER REPOSITORIO
    IGenericRepository<T> Repository<T>() where T : class;

    // El método para guardar los cambios se mantiene igual
    Task<int> CompleteAsync();
}