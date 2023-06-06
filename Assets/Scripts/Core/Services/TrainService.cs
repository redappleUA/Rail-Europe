using Core.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrainService
{
    private static TrainModel _trainModel = new();
    public static List<TrainView> Trains => _trainModel.Elements;

    public static void SetTrainPosition(TrainView train, Vector2 position)
    {
        train.transform.position = position;
    }
}
