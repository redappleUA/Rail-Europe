using Cysharp.Threading.Tasks;
using RDG;
using UnityEngine;
using UnityEngine.UIElements;

public class CityInfoController : InfoController
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] TrainInfoController _trainInfoController;
    [SerializeField] TapController _tapController;
    [SerializeField] VibrateController _vibrateController;

    private CityNameReference _clickedCity;

    private void Start()
    {
        _tapController.OnOneTap += Open;
        Activate();
    }

    protected override void Open(ClickableObject clickableObject)
    {
        if(!_trainInfoController.gameObject.activeSelf && clickableObject.TryGetComponent(out CityNameReference city))
        {
            Activate();
            if (gameObject.activeSelf)
            {
                _clickedCity = city;
                Initialize();
            }
            _ = ChangeSprite();

            Vibration.Vibrate(_vibrateController.VibrateDuration);
        }
    }

    public override void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        var cityName = root.Q<Label>("CityName");
        cityName.text = _clickedCity.CityName.ToString();

        for (int i = 0; i < _clickedCity.Passengers.Count; i++)
        {
            _passengers[i] = root.Q<VisualElement>($"Passenger{i + 1}");

            var cityIcon = _passengers[i].Q<VisualElement>("CityIcon");
            var cityTo = _passengers[i].Q<Label>("CityTo");
            var citySprite = _clickedCity.Passengers[i].CitySprite;

            cityIcon.style.backgroundImage = new StyleBackground(citySprite);
            cityTo.text = _clickedCity.Passengers[i].Passenger.CityTo.ToString();
            cityTo.style.color = GetColorFromSprite(citySprite);
        }
    }

    protected override async UniTaskVoid ChangeSprite()
    {
        if (_clickedCity.CitySpriteRenderer.sprite == _clickedCity.CitySprite)
            _clickedCity.CitySpriteRenderer.sprite = await CityService.LoadCityPushSpite(_clickedCity.CityName);
        else
            _clickedCity.CitySpriteRenderer.sprite = _clickedCity.CitySprite;
    }
}
