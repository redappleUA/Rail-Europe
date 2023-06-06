using System.Collections.Generic;

public record RouteScheme
{
    public List<CityView> CitiesOnRoute { get; private init; } = new();
    public List<BaseWayView> WaysBetweenCities { get; private init; } = new();
}
