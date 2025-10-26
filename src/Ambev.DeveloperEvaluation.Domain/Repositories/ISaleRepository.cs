using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Sale sale, CancellationToken ct = default);
        Task UpdateAsync(Sale sale, CancellationToken ct = default);
        Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListAsync(
            DateTime? dateFrom, DateTime? dateTo, string? saleNumber,
            int page, int pageSize, CancellationToken ct = default);
    }
}
