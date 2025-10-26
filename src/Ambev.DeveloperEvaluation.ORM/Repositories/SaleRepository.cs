using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public sealed class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _ctx;
        public SaleRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Sale sale, CancellationToken ct = default)
        {
            await _ctx.Set<Sale>().AddAsync(sale, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken ct = default)
        {
            _ctx.Set<Sale>().Update(sale);
            await _ctx.SaveChangesAsync(ct);
        }

        public Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            _ctx.Set<Sale>().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);

        public Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken ct = default) =>
            _ctx.Set<Sale>()
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, ct);

        public async Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListAsync(
            DateTime? dateFrom, DateTime? dateTo, string? saleNumber,
            int page, int pageSize, CancellationToken ct = default)
        {
            var q = _ctx.Set<Sale>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(saleNumber))
                q = q.Where(s => s.SaleNumber == saleNumber);

            if (dateFrom.HasValue) q = q.Where(s => s.SaleDate >= dateFrom.Value);
            if (dateTo.HasValue) q = q.Where(s => s.SaleDate < dateTo.Value);

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(s => s.SaleDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
