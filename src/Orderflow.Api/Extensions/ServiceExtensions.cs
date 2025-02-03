using Orderflow.Features.AlphaVantage.Contracts;
using Orderflow.Features.AlphaVantage.Mappers;
using Orderflow.Features.AlphaVantage.Models;
using Orderflow.Features.Common.Events.Factories;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Exchanges.CreateExchange.Contracts;
using Orderflow.Features.Exchanges.CreateExchange.Events;
using Orderflow.Features.Exchanges.CreateExchange.Mappers;
using Orderflow.Features.Exchanges.CreateExchange.Services;
using Orderflow.Features.Exchanges.GetExchange.Contracts;
using Orderflow.Features.Exchanges.GetExchange.Mappers;
using Orderflow.Features.Exchanges.GetExchange.Services;
using Orderflow.Features.Exchanges.ListExchanges.Services;
using Orderflow.Features.Instruments.Common.Mappers;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Instruments.CreateInstrument.Contracts;
using Orderflow.Features.Instruments.CreateInstrument.Events;
using Orderflow.Features.Instruments.CreateInstrument.Mappers;
using Orderflow.Features.Instruments.CreateInstrument.Services;
using Orderflow.Features.Instruments.GetInstrument.Contracts;
using Orderflow.Features.Instruments.GetInstrument.Mappers;
using Orderflow.Features.Instruments.GetInstrument.Services;
using Orderflow.Features.Instruments.ListInstruments.Services;
using Orderflow.Features.Orders.Common.Interfaces;
using Orderflow.Features.Orders.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;
using Orderflow.Features.Orders.Common.Services;
using Orderflow.Features.Orders.CreateOrder.Contracts;
using Orderflow.Features.Orders.CreateOrder.Events;
using Orderflow.Features.Orders.CreateOrder.Mappers;
using Orderflow.Features.Orders.CreateOrder.Services;
using Orderflow.Features.Orders.GetOrder.Contracts;
using Orderflow.Features.Orders.GetOrder.Mappers;
using Orderflow.Features.Orders.GetOrder.Services;
using Orderflow.Features.Orders.GetOrderBook.Contracts;
using Orderflow.Features.Orders.GetOrderBook.Mappers;
using Orderflow.Features.Orders.ListOrders.Services;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.Common.Repositories;
using Orderflow.Features.Trades.CreateTrade.Events;
using Orderflow.Features.Trades.CreateTrade.Mappers;
using Orderflow.Features.Trades.CreateTrade.Services;

namespace Orderflow.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOutboxEventMapperFactory, OutboxEventMapperFactory>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IExchangeRepository, ExchangeRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();

        services.AddSingleton<IOrderBookManager, OrderBookManager>();
        services.AddScoped<IProcessTradeService, ProcessTradeService>();
        services.AddScoped<ICreateExchangeService, CreateExchangeService>();
        services.AddScoped<IGetExchangeService, GetExchangeService>();
        services.AddScoped<IListExchangesService, ListExchangesService>();
        services.AddScoped<ICreateInstrumentService, CreateInstrumentService>();
        services.AddScoped<IGetInstrumentService, GetInstrumentService>();
        services.AddScoped<IListInstrumentsService, ListInstrumentsService>();
        services.AddScoped<ICreateOrderService, CreateOrderService>();
        services.AddScoped<IGetOrderService, GetOrderService>();
        services.AddScoped<IListOrdersService, ListOrdersService>();

        services.AddScoped<IMapper<PostOrderRequest, Order>, PostOrderRequestToOrderMapper>();
        services.AddScoped<IMapper<Order, GetOrderResponse>, OrderToGetOrderResponseMapper>();
        services.AddScoped<IMapper<Order, OrderBooksOrderResponse>, OrderToOrderBookOrderResponseMapper>();
        services.AddScoped<IMapper<Order, OrderEntity>, OrderDomainToOrderDataMapper>();
        services.AddScoped<IMapper<OrderEntity, Order>, OrderDataToOrderDomainMapper>();
        services.AddScoped<IMapper<Order, OrderRaisedEvent>, OrderToOrderRaisedEventMapper>();

        services.AddScoped<IMapper<PostExchangeRequest, Exchange>, PostExchangeRequestToExchangeMapper>();
        services.AddScoped<IMapper<Exchange, GetExchangeResponse>, ExchangeToGetExchangeResponseMapper>();
        services.AddScoped<IMapper<Exchange, ExchangeEntity>, ExchangeDomainToExchangeDataMapper>();
        services.AddScoped<IMapper<ExchangeEntity, Exchange>, ExchangeDataToExchangeDomainMapper>();
        services.AddScoped<IMapper<Exchange, ExchangeCreatedEvent>, ExchangeToExchangeCreatedEventMapper>();


        services.AddScoped<IMapper<PostInstrumentRequest, Instrument>, PostInstrumentRequestToInstrumentMapper>();
        services.AddScoped<IMapper<Instrument, GetInstrumentResponse>, InstrumentToGetInstrumentResponseMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentEntity>, InstrumentDomainToInstrumentDataMapper>();
        services.AddScoped<IMapper<InstrumentEntity, Instrument>, InstrumentDataToInstrumentDomainMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentCreatedEvent>, InstrumentToInstrumentCreatedEventMapper>();

        services.AddScoped<IMapper<Trade, TradeEntity>, TradeToTradeDataMapper>();
        services.AddScoped<IMapper<Trade, TradeExecutedEvent>, TradeToTradeExecutedEventMapper>();

        services
            .AddScoped<IMapper<GetGlobalQuoteResponse, GlobalQuote>,
                GlobalQuoteResponseToGlobalQuoteDomainMapper>();
    }
}