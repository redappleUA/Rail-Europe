using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField] GameObject _trainPrefab;
    public Train Spawn()
    {
        var train = Instantiate(_trainPrefab).GetComponent<Train>();
        return train;
    }
}
