using MediatR;

namespace Hiper.Application.Core.Events
{
    public class NewProductEvent : INotification
    {
        public string ProductName { get; set; }
    }
}
