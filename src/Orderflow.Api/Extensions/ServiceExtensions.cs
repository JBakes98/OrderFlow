using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Data.Entities;
using Orderflow.Data.Repositories;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Exchange;
using Orderflow.Events.Factories;
using Orderflow.Events.Instrument;
using Orderflow.Events.Order;
using Orderflow.Events.Trade;
using Orderflow.Mappers;
using Orderflow.Mappers.AlphaVantage;
using Orderflow.Mappers.Api.Request;
using Orderflow.Mappers.Api.Response;
using Orderflow.Mappers.Data;
using Orderflow.Mappers.Domain;
using Orderflow.Mappers.Events;
using Orderflow.Services;
using Orderflow.Services.AlphaVantage.Api.Responses;
using Orderflow.Services.Interfaces;

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
        services.AddScoped<ITradeService, TradeService>();
        services.AddScoped<IExchangeService, ExchangeService>();
        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IMapper<PostOrderRequest, Order>, PostOrderRequestToOrderMapper>();
        services.AddScoped<IMapper<Order, GetOrderResponse>, OrderToGetOrderResponseMapper>();
        services.AddScoped<IMapper<Order, OrderEntity>, OrderDomainToOrderDataMapper>();
        services.AddScoped<IMapper<OrderEntity, Order>, OrderDataToOrderDomainMapper>();
        services.AddScoped<IMapper<Order, OrderRaisedEvent>, OrderToOrderRaisedEventMapper>();

        services.AddScoped<IMapper<PostExchangeRequest, Exchange>, PostExchangeRequestToExchangeMapper>();
        services.AddScoped<IMapper<Exchange, GetExchangeResponse>, ExchangeToGetExchangeResponseMapper>();
        services.AddScoped<IMapper<Exchange, ExchangeEntity>, ExchangeDomainToExchangeDataMapper>();
        services.AddScoped<IMapper<ExchangeEntity, Exchange>, ExchangeDataToExchangeDomainMapper>();
        services.AddScoped<IMapper<Exchange, ExchangeCreatedEvent>, ExchangeToExchangeCreatedEventMapper>();


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