using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class HUDController : MonoBehaviour, IUIController
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] InfoHUDController _infoController;
    [SerializeField] PauseScreenController _pauseController;
    [SerializeField] int _secondsToIncreaseDayCount;

    private Button _pauseButton, _infoButton;
    private Label _railCount, _trainCount, _bridgeCount, _passengerCount, _dayCount;

    void Start()
    {
        Initialize();
        StartCoroutine(IncreaseDayCount());
    }

    void Update()
    {
        _railCount.text = ResourcesData.RailCount.ToString();
        _trainCount.text = ResourcesData.TrainCount.ToString();
        _bridgeCount.text = ResourcesData.BridgeCount.ToString();

        _passengerCount.text = PassengerService.ArrivedPassengers.ToString();
    }
    
    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        _pauseButton = root.Q<Button>("PauseButton");
        _infoButton = root.Q<Button>("InfoButton");
        _railCount = root.Q<Label>("RailCount");
        _trainCount = root.Q<Label>("TrainCount");
        _bridgeCount = root.Q<Label>("BridgeCount");
        _passengerCount = root.Q<Label>("PassengerCount");
        _dayCount = root.Q<Label>("DayCount");

        _infoButton.clicked += _infoController.Activate;
        _pauseButton.clicked += _pauseController.Activate;
    }

    IEnumerator IncreaseDayCount()
    {
        int dayCount = 1;

        //while (_defeat)
        while(true)
        {
            yield return new WaitForSeconds(_secondsToIncreaseDayCount);

            dayCount++;
            _dayCount.text = dayCount.ToString();
        }
    }
}
