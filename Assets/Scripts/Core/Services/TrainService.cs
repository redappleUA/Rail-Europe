using Core.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrainService
{
    private static TrainModel _trainModel = new();
    public static List<Train> Trains => _trainModel.Elements;

    public static void SetTrainPosition(Train train, Vector2 position)
    {
        train.transform.position = position;
    }
}
