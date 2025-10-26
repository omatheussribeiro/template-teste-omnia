﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem
{
    public sealed class CancelItemCommand : IRequest<CancelItemResult>
    {
        public Guid SaleId { get; }
        public Guid ItemId { get; }
        public CancelItemCommand(Guid saleId, Guid itemId)
        {
            SaleId = saleId; ItemId = itemId;
        }
    }
}
