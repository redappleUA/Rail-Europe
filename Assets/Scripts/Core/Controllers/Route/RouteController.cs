using Core.Models;
using RDG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : UIPopUp
{
    public event TrainAction<Vector2> TrainSpawn;
    public delegate Train TrainAction<T>(T component);

    [SerializeField] TapController _tapController;
    [SerializeField] TrainController _trainController;
    [SerializeField] VibrateController _vibrateController;

    private List<CityNameReference> _cities = new();
    private Route _route = new();
    private Train _train;

    private void Awake()
    {
        _tapController.OnTapMoved += AddCityToList;
        _tapController.OnTapEnded += MakeRoute;

        TrainSpawn += _trainController.HandleTrain;
    }

    private void AddCityToList(ClickableObject clickableObject)
    {
        var city = CityService.GetCityNameReferenceFromObject(clickableObject);

        if (city != null && !_cities.Contains(city))
        {
            _cities.Add(city);
        }
        Vibration.Vibrate(_vibrateController.VibrateDuration, _vibrateController.Amplitude);
    }

    private void MakeRoute(ClickableObject _)
    {
        if (CheckWaysOnActivity() && CheckInactiveRailBetweenCities())
        {
            //Додаємо міста в потяг
            foreach (var city in _cities)
            {
                _route.CitiesOnRoute.Add(city);
            }
            //Додаємо шляхи в маршрут
            AddWaysToRoute();

            //Перевіряємо чи існує такий маршрут вже
            if (RouteService.CheckSimilarRoute(_route))
            {
                _cities.Clear();
                return;
            }

            //Додаємо маршрут в модель
            RouteService.Routes.Add(_route);

            //Створюємо потяг на маршруті
            if (ResourcesData.TrainCount > 0)
            {
                _train = TrainSpawn(_route.WaysBetweenCities[0].transform.position);
                ResourcesData.TrainCount--;
            }
            else Debug.Log("Not enough resources"); //TODO: UI output

            //Додаємо маршрут в потяг
            _train.Route = _route;

            Vibration.Vibrate(_vibrateController.VibrateDuration, _vibrateController.Amplitude);

            Debug.LogWarning("Route is created - fisrt: " + _train.Route.CitiesOnRoute[0] + "; last: " + _train.Route.CitiesOnRoute[^1] +
                ". Cities Count: " + _train.Route.CitiesOnRoute.Count);
        }
        else if(_cities.Count > 2)
        {
            string text = "Route not created";
            Debug.LogWarning(text);
            OnTextPopUp.Invoke(text);
        }

        //Чистимо список, щоб якщо false - заново створити маршрут
        _cities.Clear();
        _route = new();
    }
    /// <summary>
    /// Перевіряє чи всі колії є активні на сцені
    /// </summary>
    /// <returns></returns>
    private bool CheckWaysOnActivity()
    {
        for(int i = 0; i < _cities.Count - 1; i++)
        {
            City cityFrom = _cities[i].CityName, cityTo = _cities[i + 1].CityName;

            if(WayService.TryGetRail(cityFrom, cityTo, out Rail rail))
            {
                if(rail.gameObject.activeSelf) continue;
                else return false;
            }
            else if (WayService.TryGetBridge(cityFrom, cityTo, out Bridge bridge))
            {
                if (bridge.gameObject.activeSelf) continue;
                else return false;
            }
            return false;
        }
        return true;
    }
    /// <summary>
    /// Перевіряє чи немає між першим та останнім містом списку неактивної рельси
    /// </summary>
    /// <returns></returns>
    private bool CheckInactiveRailBetweenCities()
    {
        City cityFrom = _cities[0].CityName, cityTo = _cities[^1].CityName;

        if(WayService.TryGetRail(cityFrom, cityTo, out Rail rail))
        {
            if(!rail.gameObject.activeSelf)
                return false;
        }
        else if (WayService.TryGetBridge(cityFrom, cityTo, out Bridge bridge))
        {
            if (!bridge.gameObject.activeSelf)
                return false;
        }
        return true;
    }

    private void AddWaysToRoute()
    {
        for (int i = 0; i < _cities.Count - 1; i++)
        {
            City cityFrom = _cities[i].CityName, cityTo = _cities[i + 1].CityName;

            if (WayService.TryGetRail(cityFrom, cityTo, out Rail rail))
            {
                _route.WaysBetweenCities.Add(rail);
            }
            else if (WayService.TryGetBridge(cityFrom, cityTo, out Bridge bridge))
            {
                _route.WaysBetweenCities.Add(bridge);
            }
        }
    }
}