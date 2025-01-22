using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface ICreateOrderService
{
    Task<OneOf<Order, Error>> CreateOrder(Order order);
}