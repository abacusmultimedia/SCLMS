 
using Microsoft.EntityFrameworkCore;
using Sclms.Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;


public class Repository<T> : IRepository<T> where T : class
{
    private readonly DrillingDBContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DrillingDBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public  IQueryable<T>  GetAllAsync()
    {
        return  _dbSet.AsQueryable()  ;
    }

 
}
