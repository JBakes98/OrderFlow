namespace Orderflow.Domain;

public static class ErrorCodes
{
    public const string ExchangeCouldNotBeCreated = "exchange_could_not_be_created";
    public const string ExchangeNotFound = "exchange_not_found";


    public const string InstrumentNotFound = "instrument_not_found";
    public const string InstrumentCouldNotBeCreated = "instrument_could_not_be_created";

    public const string OrderCouldNotBeCreated = "order_could_not_be_created";
    public const string OrderCouldNotBeUpdated = "order_could_not_be_updated";
    public const string OrderNotFound = "order_not_found";

    public const string OrderFileUploadFailed = "order_file_upload_failed";
    public const string SpecifiedBucketDoesNotExist = "specified_bucket_does_not_exist";

    public const string UnableToRetrieveCurrentInstrumentPrice = "unable_to_retrieve_current_instrument_price";

    public const string TradeExecutionFailed = "trade_execution_failed";
}