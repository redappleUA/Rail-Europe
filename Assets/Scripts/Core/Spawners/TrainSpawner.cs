using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField] GameObject _trainPrefab;
    public TrainView Spawn()
    {
        var train = Instantiate(_trainPrefab).GetComponent<TrainView>();
        return train;
    }
}
