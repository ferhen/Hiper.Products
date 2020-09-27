using AutoMapper;
using Hiper.Application.Core.Events;
using Hiper.Application.Data;
using Hiper.Application.Domain.EventHandlers.Base;
using Hiper.Application.Domain.Services;
using Hiper.Application.Presentation.DTO;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.Application.Domain.EventHandlers
{
    public class NewOrUpdatedProductEventHandler : EventHandlerBase, INotificationHandler<NewOrUpdatedProductEvent>
    {
        private readonly IMapper _mapper;
        private readonly PublisherService _publisherService;

        public NewOrUpdatedProductEventHandler(IMapper mapper, PublisherService publisherService, IUnitOfWork uow) : base(uow)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        public async Task Handle(NewOrUpdatedProductEvent receivedEvent, CancellationToken cancellationToken)
        {
            ProductDTO product;

            if (string.IsNullOrWhiteSpace(receivedEvent.ProductName))
            {
                product = _mapper.Map<ProductDTO>(
                    await _uow.Products.GetByIdIncludeStock(receivedEvent.ProductId)
                );
            }
            else
            {
                product = new ProductDTO
                {
                    ProductName = receivedEvent.ProductName,
                    StockQuantity = receivedEvent.StockQuantity
                };
            }

            _publisherService.Publish(JsonConvert.SerializeObject(product));

            await Task.CompletedTask;
        }
    }
}
