using Models;

namespace Repositories;

public interface IRepository<T> where T : BaseModel
{
    // Pegar todos clientes
    Task<List<T>> GetAllClientsAsync();

    // Pegar pelo Id
    Task<T?> GetClientById(int id);

    // Add Client
    Task AddClient(T client);

    // Update Client
    Task UpdateClient(T client);

    // Remove client
    Task RemoveClient(T client);
}