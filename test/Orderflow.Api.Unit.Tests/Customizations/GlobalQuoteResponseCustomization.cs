using System.Globalization;
using AutoFixture;
using Orderflow.Contracts.Responses.AlphaVantage;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class GlobalQuoteResponseCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new GlobalQuote
        {
            Symbol = fixture.Create<string>(),
            Open = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            High = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            Low = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            Price = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            Volume = fixture.Create<int>().ToString(CultureInfo.InvariantCulture),
            LatestTradingDate = fixture.Create<DateTime>().ToString(CultureInfo.InvariantCulture),
            PrevClose = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            Change = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
            ChangePerc = fixture.Create<double>().ToString(CultureInfo.InvariantCulture),
        });
    }
}