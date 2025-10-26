using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelItem;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/sales")]
    public sealed class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // POST /api/sales
        [HttpPost]
        public async Task<ActionResult<CreateSaleResponse>> Create([FromBody] CreateSaleRequest request, CancellationToken ct)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, ct);
            var response = _mapper.Map<CreateSaleResponse>(result);
            return CreatedAtAction(nameof(GetById), new { id = response.SaleId }, response);
        }

        // GET /api/sales/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSaleResponse>> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var query = new GetSaleQuery(id);
            var result = await _mediator.Send(query, ct);
            var response = _mapper.Map<GetSaleResponse>(result);
            return Ok(response);
        }

        // GET /api/sales?Page=1&PageSize=10&SaleNumber=...&DateFrom=...&DateTo=...
        [HttpGet]
        public async Task<ActionResult<ListSalesResponse>> List([FromQuery] ListSalesRequest request, CancellationToken ct)
        {
            var query = _mapper.Map<ListSalesQuery>(request);
            var result = await _mediator.Send(query, ct);
            var response = _mapper.Map<ListSalesResponse>(result);
            return Ok(response);
        }

        // PUT /api/sales/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateSaleResponse>> Update([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken ct)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.SaleId = id;

            var result = await _mediator.Send(command, ct);
            var response = _mapper.Map<UpdateSaleResponse>(result);
            return Ok(response);
        }

        // POST /api/sales/{id}/cancel
        [HttpPost("{id:guid}/cancel")]
        public async Task<ActionResult<CancelSaleResponse>> CancelSale([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new CancelSaleCommand(id);
            var result = await _mediator.Send(cmd, ct);
            var response = _mapper.Map<CancelSaleResponse>(result);
            return Ok(response);
        }

        // POST /api/sales/{id}/items/{itemId}/cancel
        [HttpPost("{id:guid}/items/{itemId:guid}/cancel")]
        public async Task<ActionResult<CancelItemResponse>> CancelItem([FromRoute] Guid id, [FromRoute] Guid itemId, CancellationToken ct)
        {
            var cmd = new CancelItemCommand(id, itemId);
            var result = await _mediator.Send(cmd, ct);
            var response = _mapper.Map<CancelItemResponse>(result);
            return Ok(response);
        }
    }
}
