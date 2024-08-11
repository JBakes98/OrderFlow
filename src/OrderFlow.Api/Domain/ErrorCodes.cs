namespace OrderFlow.Domain;

public static class ErrorCodes
{
    public const string InstrumentNotFound = "instrument_not_found";
    public const string InstrumentCouldNotBeCreated = "instrument_could_not_be_created";
    public const string OrderCouldNotBeCreated = "order_could_not_be_created";
    public const string OrderNotFound = "order_not_found";

    public const string EventCouldNotBePublished = "event_could_not_be_published";

    public const string OrderFileUploadFailed = "order_file_upload_failed";
    public const string SpecifiedBucketDoesNotExist = "specified_bucket_does_not_exist";

    public const string UnableToRetrieveCurrentInstrumentPrice = "unable_to_retrieve_current_instrument_price";
}