using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    public event TrainAction<Vector2> TrainSpawn;
    public delegate Train TrainAction<T>(T component);

    [SerializeField] TapController _tapController;
    [SerializeField] TrainController _trainController;

    private List<City> _cities = new();
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
        var city = CityService.GetCityFromObject(clickableObject);

        if (!_cities.Contains(city))
        {
            _cities.Add(city);
        }
    }

    private void MakeRoute(ClickableObject _)
    {
        if (CheckWaysOnActivity() && CheckInactiveRailBetweenCities())
        {
            //������ ���� � �����
            foreach (var city in _cities)
            {
                _route.CitiesOnRoute.Add(city);
            }
            //������ ����� � �������
            AddWaysToRoute();

            //���������� �� ���� ����� ������� ���
            if (RouteService.CheckSimilarRoute(_route))
            {
                _cities.Clear();
                return;
            }

            //������ ������� � ������
            RouteService.Routes.Add(_route);

            //��������� ����� �� �������
            _train = TrainSpawn(_route.WaysBetweenCities[0].transform.position);

            //������ ������� � �����
            _train.Route = _route;

            ////³���������, ��� ����� �� ���������� �������� ��� ����� ������
            //_tapController.OnTapMoved -= AddCityToList;
            //_tapController.OnTapEnded -= MakeRoute;

            Debug.LogWarning("Route is created - fisrt: " + _train.Route.CitiesOnRoute[0] + "; last: " + _train.Route.CitiesOnRoute[^1] + 
                ". Cities Count: " + _train.Route.CitiesOnRoute.Count);
        }
        else Debug.LogWarning("Route not created.");

        //������� ������, ��� ���� false - ������ �������� �������
        _cities.Clear();
        _route = new();
    }
    /// <summary>
    /// �������� �� �� ��볿 � ������� �� �����
    /// </summary>
    /// <returns></returns>
    private bool CheckWaysOnActivity()
    {
        for(int i = 0; i < _cities.Count - 1; i++)
        {
            City cityFrom = _cities[i], cityTo = _cities[i + 1];

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
    /// �������� �� ���� �� ������ �� �������� ����� ������ ��������� ������
    /// </summary>
    /// <returns></returns>
    private bool CheckInactiveRailBetweenCities()
    {
        City cityFrom = _cities[0], cityTo = _cities[^1];

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
            City cityFrom = _cities[i], cityTo = _cities[i + 1];

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