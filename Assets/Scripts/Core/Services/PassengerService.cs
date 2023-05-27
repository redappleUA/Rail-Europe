using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PassengerService
{
    public static void SetPassengerTransform(Train train, PassengerAttached passenger)
    {
        for (int i = 0; i < train.PassengerPositions.Length; i++)
        {
            var positionTransform = train.PassengerPositions[i];
            if (positionTransform != null && positionTransform.childCount == 0)
            {
                passenger.transform.SetParent(positionTransform);
                passenger.transform.localPosition = Vector3.zero;
                passenger.transform.localScale = new Vector3(train.PassangerScale, train.PassangerScale, train.PassangerScale);
            }
        }
    }

    public static GameObject InstantiateAttachedPassenger()
    {
        GameObject passengerAttached = new("Passanger");
        passengerAttached.AddComponent<PassengerAttached>();
        return passengerAttached;
    }

    public static void AddPassengerToRain(ref Train train, Passenger passenger)
    {
        var scenePassenger = PassengerService.InstantiateAttachedPassenger().GetComponent<PassengerAttached>();
        scenePassenger.Construct(passenger, train);

        train.Passengers.Add(scenePassenger);
    }
}
