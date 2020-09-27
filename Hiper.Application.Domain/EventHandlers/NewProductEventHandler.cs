using AutoMapper;
using Hiper.Application.Core.Events;
using Hiper.Application.Core.Models;
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
    public class NewProductEventHandler : EventHandlerBase, INotificationHandler<NewProductEvent>
    {
        public NewProductEventHandler(IUnitOfWork uow) : base(uow) { }

        public async Task Handle(NewProductEvent receivedEvent, CancellationToken cancellationToken)
        {
            var product = await _uow.Products.GetByName(receivedEvent.ProductName);
            var stock = new Stock(0, product.Id);
            await _uow.Stocks.Add(stock);
            await _uow.SaveChanges();
        }
    }
}
