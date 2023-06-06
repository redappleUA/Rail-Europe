using Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RouteService
{
    private static RouteModel _routeModel = new();
    public static List<RouteScheme> Routes => _routeModel.Elements;

    /// <summary>
    /// Check if exist the simillar route
    /// </summary>
    /// <param name="route"></param>
    /// <returns>True if exist and false if doesnt</returns>
    public static bool CheckSimilarRoute(RouteScheme route)
    {
        // Check if the given route has similar cities and ways with any existing route
        foreach (RouteScheme existingRoute in Routes)
        {
            if (existingRoute.CitiesOnRoute.SequenceEqual(route.CitiesOnRoute) &&
                existingRoute.WaysBetweenCities.SequenceEqual(route.WaysBetweenCities))
            {
                // Similar route found
                return true;
            }
        }

        // No similar route found
        return false;
    }
}
