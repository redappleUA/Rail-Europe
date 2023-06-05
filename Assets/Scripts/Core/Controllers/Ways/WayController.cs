using System.Collections.Generic;
using RDG;
using UnityEngine;

public class WayController : UIPopUp
{
    [SerializeField] TapController _tapController;
    [SerializeField] VibrateController _vibrateController;
    public City CityFrom { get; private set; }
    public City CityTo { get; private set; }
    private readonly List<ClickableObject> _clickableObjects = new();

    private void Start()
    {
        _tapController.OnTapStarted += GetCityFromForWay;
        _tapController.OnTapMoved += AddClickableObjects;
        _tapController.OnTapEnded += TurnOnWay;
    }

    private void GetCityFromForWay(ClickableObject clickableObject)
    {
        // Set the starting city for the way
        CityFrom = CityService.GetCityFromObject(clickableObject);
    }

    private void AddClickableObjects(ClickableObject clickableObject)
    {
        if (!_clickableObjects.Contains(clickableObject))
        {
            // Add the clickableObject to the list of clickable objects
            _clickableObjects.Add(clickableObject);
        }
    }

    private void TurnOnWay(ClickableObject clickableObject)
    {
        // Get the destination city for the way
        CityTo = CityService.GetCityFromObject(clickableObject);

        // Check if the destination city is the same as the starting city
        if (CityTo == CityFrom) return;

        // Initialize variables for the rail and bridge
        (Rail rail, Bridge bridge) ways = (rail: null, bridge: null);

        // Get the rail and bridge objects for the selected cities
        ways.rail = WayService.GetRail(CityFrom, CityTo);
        ways.bridge = WayService.GetBridge(CityFrom, CityTo);

        // Check if a rail can be built
        if (ways.rail != null && ResourcesData.RailCount >= ways.rail.BuildResources)
        {
            // Activate the rail object
            ways.rail.gameObject.SetActive(true);
            ResourcesData.RailCount -= ways.rail.BuildResources;

            //Vibration.Vibrate(_vibrateController.VibrateDuration, _vibrateController.Amplitude);
        }
        // Check if a bridge can be built
        else if (ways.bridge != null && ResourcesData.BridgeCount >= ways.bridge.BuildResources)
        {
            // Activate the bridge object
            ways.bridge.gameObject.SetActive(true);
            ResourcesData.BridgeCount -= ways.bridge.BuildResources;

            //Vibration.Vibrate(_vibrateController.VibrateDuration, _vibrateController.Amplitude);
        }
        else
        {
            // Handle the case where a way cannot be built
            if (_clickableObjects.Count <= 2)
            {
                string text = "Way cannot be built";
                Debug.LogWarning(text);
                OnTextPopUp.Invoke(text);
            }
            _clickableObjects.Clear();
        }
    }
}
