using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerController : MonoBehaviour
{
    [SerializeField] Train _train;
    [SerializeField] TrainMoveController _trainMoveController;

    private const int MAX_COUNT = 3;

    private void Start()
    {
        _trainMoveController.OnTrainStop += TrainStop;

        AddPassangersAtStart();
    }

    private void TrainStop(Way way)
    {
        #region Check for destination

        for (int i = _train.Passengers.Count - 1; i >= 0; i--)
        {
            var passenger = _train.Passengers[i];

            if (passenger.CityTo == way.CityA || passenger.CityTo == way.CityB)
            {
                Debug.LogWarning("Passenger arrived from: " + passenger.CitySpawn + "; To: " + passenger.CityTo);

                _train.Passengers.RemoveAt(i);
            }
        }
        #endregion

        #region Add passangers
        if (_train.Passengers.Count < MAX_COUNT)
        {
            var cityNameRef = CityService.GetCityNameReference(CityService.GetCurrentCity(_train, way));

            foreach (var passanger in cityNameRef.Passangers)
            {
                if (_train.Route.CitiesOnRoute.ContainsCity(passanger.CityTo))
                {
                    Debug.LogWarning("Passanger has boarded a train:" + _train.Route.CitiesOnRoute[0].CityName + ", " + _train.Route.CitiesOnRoute[^1].CityName
                    + "; from: " + passanger.CitySpawn + "; To: " + passanger.CityTo);

                    _train.Passengers.Add(passanger);

                    if (_train.Passengers.Count == MAX_COUNT) break;
                }
            }
        }
        #endregion

        Debug.Log("Train passangers count: " + _train.Passengers.Count);
    }

    private void AddPassangersAtStart()
    {
        int counter = 0;

        foreach(var passanger in _train.Route.CitiesOnRoute[0].Passangers)
        {
            if (_train.Route.CitiesOnRoute.ContainsCity(passanger.CityTo))
            {
                _train.Passengers.Add(passanger);
                counter++;
            }

            if (_train.Passengers.Count == MAX_COUNT) break;
        }
        if(counter > 0)
            _train.Route.CitiesOnRoute[0].Passangers.RemoveRange(0, counter);
    }
}
