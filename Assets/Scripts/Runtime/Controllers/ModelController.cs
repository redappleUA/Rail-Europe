using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private void OnDestroy()
    {
        PassengerService.ArrivedPassengers = 0;
        ScoreService.Score = 0;

        CityService.Cities.Clear();
        RouteService.Routes.Clear();
        TrainService.Trains.Clear();
        WayService.Rails.Clear();
        WayService.Bridges.Clear();
    }
}
