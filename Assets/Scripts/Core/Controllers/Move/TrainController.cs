using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] TrainSpawner _spawner;

    // Handle the train by spawning it at the specified position
    public Train HandleTrain(Vector2 trainPosition)
    {
        // Spawn a new train using the TrainSpawner
        var train = _spawner.Spawn();

        // Set the position of the train using TrainService
        TrainService.SetTrainPosition(train, trainPosition);

        // Add the train to the Trains list in TrainService
        TrainService.Trains.Add(train);

        // Return the spawned train
        return train;
    }
}
