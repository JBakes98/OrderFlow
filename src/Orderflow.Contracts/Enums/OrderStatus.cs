using System.Text.Json.Serialization;

namespace Orderflow.Contracts.Enums;

public enum OrderStatus
{
    pending,
    complete,
    cancelled
}