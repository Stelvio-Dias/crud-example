
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly KambaiaAPIDbContext _context;
    private readonly DbSet<T> _set;

    public Repository(KambaiaAPIDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }


    public async Task AddClient(T client)
    {
        await _context.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllClientsAsync()
        => await _set.ToListAsync();

    public async Task<T?> GetClientById(int id)
        => await _set.AsNoTracking().FirstAsync(item => item.Id == id);

    public async Task RemoveClient(T client)
    {
        _context.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClient(T client)
    {
        _context.Update(client);
        await _context.SaveChangesAsync();
    }
}