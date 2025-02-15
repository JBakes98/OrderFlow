using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Orderflow.Common.Extensions;

public static class MetricExtensions
{
    public static Meter SMeter = new("Orderflow.Orderbook", "1.0.0");

    public static Counter<int> CountOrdersPlaced = SMeter.CreateCounter<int>(
        name: "orders.count",
        description: "Counts the number of orders");

    public static ActivitySource OrderActivitySource = new("Orderflow.Example");
}