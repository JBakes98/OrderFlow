namespace OrderFlow.Domain;

public static class ErrorCodes
{
    public const string InstrumentNotFound = "instrument_not_found";
    public const string OrderNotFound = "order_not_found";
    public const string OrderCreationFailed = "order_creation_failed";
    public const string EventNotFound = "Event_not_found";
    public const string EventCouldNotBePublished = "Event_could_not_be_published";
}