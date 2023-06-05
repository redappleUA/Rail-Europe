using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    [SerializeField] int _timeToIncreaseRails, _timeToIncreaseBridge, _timeToIncreaseTrain;
    [SerializeField] DefeatController _defeatController;

    void Start()
    {
        StartCoroutine(IncreaseRails());
        StartCoroutine(IncreaseBridges());
        StartCoroutine(IncreaseTrains());
    }

    IEnumerator IncreaseRails()
    {
        while (!_defeatController.IsDefeat)
        {
            yield return new WaitForSeconds(_timeToIncreaseRails);

            ResourcesData.RailCount++;
        }
    }
    IEnumerator IncreaseBridges()
    {
        while (!_defeatController.IsDefeat)
        {
            yield return new WaitForSeconds(_timeToIncreaseBridge);

            ResourcesData.BridgeCount++;
        }
    }
    IEnumerator IncreaseTrains()
    {
        while (!_defeatController.IsDefeat)
        {
            yield return new WaitForSeconds(_timeToIncreaseTrain);

            ResourcesData.TrainCount++;
        }
    }
}
