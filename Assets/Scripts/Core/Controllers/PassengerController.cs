using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField] Train _train;
    [SerializeField] TrainMoveController _trainMoveController;

    private const int MAX_COUNT = 3;

    private void Start()
    {
        _trainMoveController.OnTrainStop += TrainStop;

        AddPassangersAtStart();
    }

    private void TrainStop(Way way) => StartCoroutine(TrainStopCoroutine(way));

    private IEnumerator TrainStopCoroutine(Way way)
    {
        #region Check for destination

        for (int i = _train.Passengers.Count - 1; i >= 0; i--)
        {
            var passenger = _train.Passengers[i].Passenger;

            if (passenger.CityTo == way.CityA || passenger.CityTo == way.CityB)
            {
                Debug.LogWarning("Passenger arrived from: " + passenger.CitySpawn + "; To: " + passenger.CityTo);

                Destroy(_train.Passengers[i].gameObject);
                yield return new WaitUntil(() => _train.Passengers[i] == null);
                Debug.Log("Passengers count after destroying: " + _train.Passengers.Count);

                _train.Passengers.RemoveAt(i);
                Debug.Log("Passengers count after removing: " + _train.Passengers.Count);
            }
        }
        #endregion

        #region Add passengers
        if (_train.Passengers.Count < MAX_COUNT)
        {
            var cityNameRef = CityService.GetCityNameReference(CityService.GetCurrentCity(_train, way));

            foreach (var passenger in cityNameRef.Passengers)
            {
                if (_train.Route.CitiesOnRoute.ContainsCity(passenger.CityTo))
                {
                    Debug.LogWarning("Passanger has boarded a train:" + _train.Route.CitiesOnRoute[0].CityName + ", " + _train.Route.CitiesOnRoute[^1].CityName
                    + "; from: " + passenger.CitySpawn + "; To: " + passenger.CityTo);

                    PassengerService.AddPassengerToRain(ref _train, passenger);

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

        foreach(var passenger in _train.Route.CitiesOnRoute[0].Passengers)
        {
            if (_train.Route.CitiesOnRoute.ContainsCity(passenger.CityTo))
            {
                PassengerService.AddPassengerToRain(ref _train, passenger);
                counter++;
            }

            if (_train.Passengers.Count == MAX_COUNT) break;
        }
        if(counter > 0)
            _train.Route.CitiesOnRoute[0].Passengers.RemoveRange(0, counter);
    }
}
