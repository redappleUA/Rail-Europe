using Cysharp.Threading.Tasks;
using RDG;
using Services;
using UnityEngine;
using UnityEngine.UIElements;

public class TrainInfoController : InfoController
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] CityInfoController _cityInfoController;
    [SerializeField] TapController _tapController;
    [SerializeField] VibrateController _vibrateController;

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

            Vibration.Vibrate(_vibrateController.VibrateDuration, _vibrateController.Amplitude);
        }
    }

    public override void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        // Set up UI elements for City A
        var cityA = root.Q<VisualElement>("CityA");
        var cityAIcon = cityA.Q<VisualElement>("CityIcon");
        var cityAText = cityA.Q<Label>("City");
        var citySprite = _clickedTrain.Route.CitiesOnRoute[0].CitySprite;

        cityAIcon.style.backgroundImage = new StyleBackground(citySprite);
        cityAText.text = _clickedTrain.Route.CitiesOnRoute[0].CityName.ToString();
        cityAText.style.color = GetColorFromSprite(citySprite);

        // Set up UI elements for City B
        var cityB = root.Q<VisualElement>("CityB");
        var cityBIcon = cityB.Q<VisualElement>("CityIcon");
        var cityBText = cityB.Q<Label>("City");
        citySprite = _clickedTrain.Route.CitiesOnRoute[^1].CitySprite;

        cityBIcon.style.backgroundImage = new StyleBackground(citySprite);
        cityBText.text = _clickedTrain.Route.CitiesOnRoute[^1].CityName.ToString();
        cityBText.style.color = GetColorFromSprite(citySprite);

        // Set up UI elements for passengers
        for (int i = 0; i < _clickedTrain.Passengers.Count; i++)
        {
            _passengers[i] = root.Q<VisualElement>($"Passenger{i + 1}");

            var cityIcon = _passengers[i].Q<VisualElement>("CityIcon");
            var cityTo = _passengers[i].Q<Label>("CityTo");
            citySprite = _clickedTrain.Passengers[i].CitySprite;

            cityIcon.style.backgroundImage = new StyleBackground(citySprite);
            cityTo.text = _clickedTrain.Passengers[i].Passenger.CityTo.ToString();
            cityTo.style.color = GetColorFromSprite(citySprite);
        }
    }

    protected override async UniTaskVoid ChangeSprite()
    {
        // Check if the current sprite is the same as the clicked train's sprite
        if (_clickedTrain.CitySpriteRenderer.sprite == _clickedTrain.CitySprite)
        {
            // Load and set a different sprite
            _clickedTrain.CitySpriteRenderer.sprite = await LoadResourceService.LoadSprite("trains/trains_dont_turn/train_push1");
        }
        else
        {
            // Set the original sprite
            _clickedTrain.CitySpriteRenderer.sprite = _clickedTrain.CitySprite;
        }
    }
}