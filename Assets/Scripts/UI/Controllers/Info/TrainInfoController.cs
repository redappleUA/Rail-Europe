using Cysharp.Threading.Tasks;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrainInfoController : InfoController
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] CityInfoController _cityInfoController;
    [SerializeField] TapController _tapController;

    private Train _clickedTrain;

    private void Start()
    {
        _tapController.OnOneTap += Open;
        Activate();
    }

    protected override void Open(ClickableObject clickableObject)
    {
        if (!_cityInfoController.gameObject.activeSelf && clickableObject.TryGetComponent(out Train train))
        {
            Activate();
            if (gameObject.activeSelf)
            {
                _clickedTrain = train;
                Initialize();
            }
            _ = ChangeSprite();
        }
    }

    public override void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        var cityA = root.Q<VisualElement>("CityA");
        var cityB = root.Q<VisualElement>("CityB");

        {
            var cityAIcon = cityA.Q<VisualElement>("CityIcon");
            var cityAText = cityA.Q<Label>("City");
            var citySprite = _clickedTrain.Route.CitiesOnRoute[0].CitySprite;

            cityAIcon.style.backgroundImage = new StyleBackground(citySprite);
            cityAText.text = _clickedTrain.Route.CitiesOnRoute[0].CityName.ToString();
            cityAText.style.color = GetColorFromSprite(citySprite);
        }
        {
            var cityBIcon = cityB.Q<VisualElement>("CityIcon");
            var cityBText = cityB.Q<Label>("City");
            var citySprite = _clickedTrain.Route.CitiesOnRoute[^1].CitySprite;

            cityBIcon.style.backgroundImage = new StyleBackground(citySprite);
            cityBText.text = _clickedTrain.Route.CitiesOnRoute[^1].CityName.ToString();
            cityBText.style.color = GetColorFromSprite(citySprite);
        }

        for (int i = 0; i < _clickedTrain.Passengers.Count; i++)
        {
            _passengers[i] = root.Q<VisualElement>($"Passenger{i + 1}");

            var cityIcon = _passengers[i].Q<VisualElement>("CityIcon");
            var cityTo = _passengers[i].Q<Label>("CityTo");
            var citySprite = _clickedTrain.Passengers[i].CitySprite;

            cityIcon.style.backgroundImage = new StyleBackground(citySprite);
            cityTo.text = _clickedTrain.Passengers[i].Passenger.CityTo.ToString();
            cityTo.style.color = GetColorFromSprite(citySprite);
        }
    }

    protected override async UniTaskVoid ChangeSprite()
    {
        if (_clickedTrain.CitySpriteRenderer.sprite == _clickedTrain.CitySprite)
            _clickedTrain.CitySpriteRenderer.sprite = await LoadResourceService.LoadSprite("trains/trains_dont_turn/train_push1");
        else
            _clickedTrain.CitySpriteRenderer.sprite = _clickedTrain.CitySprite;
    }
}
