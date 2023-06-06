using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField] TrainView _train;
    [SerializeField] TrainMoveController _trainMoveController;

    private const int MAX_COUNT = 3;

    private void Start()
    {
        _trainMoveController.OnTrainStop += TrainStop;

        AddPassangersAtStart();
    }

    private void TrainStop(BaseWayView way) => StartCoroutine(TrainStopCoroutine(way));

    private IEnumerator TrainStopCoroutine(BaseWayView way)
    {
        #region Check for destination

        for (int i = _train.Passengers.Count - 1; i >= 0; i--)
        {
            var passenger = _train.Passengers[i].Passenger;

            if (passenger.CityTo == way.CityA || passenger.CityTo == way.CityB)
            {
                Debug.LogWarning($"Passenger arrived from: {passenger.CitySpawn}; To: {passenger.CityTo}");

                Destroy(_train.Passengers[i].gameObject);
                yield return new WaitUntil(() => _train.Passengers[i] == null);

                _train.Passengers.RemoveAt(i);
                PassengerService.ArrivedPassengers++;
            }
        }
        #endregion

        #region Add passengers
        if (_train.Passengers.Count < MAX_COUNT)
        {
            var cityNameRef = CityService.GetCityView(CityService.GetCurrentCity(_train, way));

            for (int i = 0; i < cityNameRef.Passengers.Count; i++)
            {
                var passenger = cityNameRef.Passengers[i];
                if (_train.Route.CitiesOnRoute.ContainsCity(passenger.Passenger.CityTo))
                {
                    Debug.LogWarning($"Passenger has boarded a train: {_train.Route.CitiesOnRoute[0].CityName}, {_train.Route.CitiesOnRoute[^1].CityName};" +
                        $"from:  {passenger.Passenger.CitySpawn}; To: {passenger.Passenger.CityTo}");

                    PassengerService.SetPassengerTransform(_train, passenger);
                    cityNameRef.Passengers.RemoveAt(i);
                    _train.Passengers.Add(passenger);
                    passenger.Train = _train;

                    if (_train.Passengers.Count == MAX_COUNT)
                        break;
                }
            }
        }
        #endregion
    }

    private void AddPassangersAtStart()
    {
        int counter = 0;

        foreach(var passenger in _train.Route.CitiesOnRoute[0].Passengers)
        {
            if (_train.Route.CitiesOnRoute.ContainsCity(passenger.Passenger.CityTo))
            {
                PassengerService.SetPassengerTransform(_train, passenger);
                _train.Passengers.Add(passenger);
                counter++;
            }

            if (_train.Passengers.Count == MAX_COUNT) break;
        }
        if(counter > 0)
            _train.Route.CitiesOnRoute[0].Passengers.RemoveRange(0, counter);
    }
}
