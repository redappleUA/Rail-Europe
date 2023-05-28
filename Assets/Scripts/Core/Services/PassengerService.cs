using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PassengerService
{
    private const int MAX_SPAWN_COUNT = 4;
    public static void SetPassengerTransform(IPassengerPosition positions, PassengerAttached passenger)
    {
        //for (int i = 0; i < positions.PassengerPositions.Length; i++)
        for (int i = positions.PassengerPositions.Length - 1; i >= 0; i--)
        {
            var positionTransform = positions.PassengerPositions[i];
            if (positionTransform != null && positionTransform.childCount == 0)
            {
                passenger.transform.SetParent(positionTransform);
                passenger.transform.localPosition = Vector3.zero;
                passenger.transform.localScale = Vector3.one;
            }
        }
    }

    public static GameObject InstantiateAttachedPassenger()
    {
        GameObject passengerAttached = new("Passanger");
        passengerAttached.AddComponent<PassengerAttached>();
        return passengerAttached;
    }

    public static bool CheckForMaxSpawnCount(CityNameReference city)
    {
        return city.Passengers.Count >= MAX_SPAWN_COUNT;
    }
}
