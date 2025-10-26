using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Common;     // BaseEntity (com DomainEvents de INotification)
using Ambev.DeveloperEvaluation.Domain.Enums;      // SaleStatus
using Ambev.DeveloperEvaluation.Domain.Events;     // SaleCreatedEvent, SaleModifiedEvent, etc.
using Ambev.DeveloperEvaluation.Domain.Services;   // IDiscountService

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public string SaleNumber { get; private set; } = string.Empty;
        public DateTime SaleDate { get; private set; } = DateTime.UtcNow;

        public Guid ClientId { get; private set; }
        public string ClientName { get; private set; } = string.Empty;

        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; } = string.Empty;

        public decimal TotalAmount { get; private set; }
        public SaleStatus Status { get; private set; } = SaleStatus.Active;

        // EF
        protected Sale() { }

        public Sale(Guid id, string saleNumber, DateTime saleDate, Guid clientId, string clientName, Guid branchId, string branchName)
        {
            // garante atribuição do Id (você tinha o parâmetro id mas não setava)
            Id = id == Guid.Empty ? Guid.NewGuid() : id;

            SaleNumber = saleNumber;
            SaleDate = saleDate;
            ClientId = clientId;
            ClientName = clientName ?? string.Empty;
            BranchId = branchId;
            BranchName = branchName ?? string.Empty;
            Status = SaleStatus.Active;

            AddDomainEvent(new SaleCreatedEvent(Id));
        }

        /// <summary>
        /// Adiciona um item aplicando a política de desconto do domínio.
        /// Se o produto já existir (não cancelado), consolida a quantidade.
        /// </summary>
        public void AddItem(IDiscountService discountService, Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (discountService is null) throw new ArgumentNullException(nameof(discountService));
            if (Status == SaleStatus.Cancelled)
                throw new InvalidOperationException("Não é possível adicionar itens a uma venda cancelada.");

            var existing = _items.FirstOrDefault(i => i.ProductId == productId && !i.IsCancelled);
            if (existing != null)
            {
                var newQty = existing.Quantity + quantity;

                // valida antecipadamente contra a política (lança se > 20 ou <= 0)
                discountService.GetDiscountPercent(newQty);

                existing.UpdateQuantity(discountService, newQty);
            }
            else
            {
                // valida antes de criar (lança se inválido)
                discountService.GetDiscountPercent(quantity);

                var item = new SaleItem(Id, productId, productName, unitPrice, quantity, discountService);
                _items.Add(item);
            }

            RecalculateTotals();
            AddDomainEvent(new SaleCreatedEvent(Id));
        }

        /// <summary>
        /// Atualiza a quantidade de um item existente aplicando a política de desconto.
        /// </summary>
        public void UpdateItemQuantity(IDiscountService discountService, Guid itemId, int newQuantity)
        {
            if (discountService is null) throw new ArgumentNullException(nameof(discountService));
            if (Status == SaleStatus.Cancelled)
                throw new InvalidOperationException("Não é possível alterar itens de uma venda cancelada.");

            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado.");

            // valida antecipadamente
            discountService.GetDiscountPercent(newQuantity);

            item.UpdateQuantity(discountService, newQuantity);
            RecalculateTotals();

            AddDomainEvent(new SaleModifiedEvent(Id));
        }

        public void CancelItem(Guid itemId)
        {
            if (Status == SaleStatus.Cancelled)
                throw new InvalidOperationException("Não é possível cancelar itens de uma venda já cancelada.");

            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado.");

            item.Cancel();
            RecalculateTotals();

            AddDomainEvent(new ItemCancelledEvent(Id, itemId));
        }

        public void CancelSale(Guid itemId)
        {
            if (Status == SaleStatus.Cancelled)
                throw new InvalidOperationException("A venda já está cancelada.");

            Status = SaleStatus.Cancelled;

            AddDomainEvent(new SaleCancelledEvent(Id, itemId));
        }

        public void RecalculateTotals()
        {
            TotalAmount = _items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.Total);
        }
    }
}
