using Core.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour 
{
    [SerializeField] TrainSpawner _spawner;

    public Train HandleTrain(Vector2 trainPosition)
    {
        var train = _spawner.Spawn();
        TrainService.SetTrainPosition(train, trainPosition);

        TrainService.Trains.Add(train);
        return train;
    }
}
