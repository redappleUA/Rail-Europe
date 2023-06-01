using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CityInfoController : MonoBehaviour, IUIController, IUIActivator
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] TapController _tapController;

    private VisualElement[] _passengers = new VisualElement[4];
    private CityNameReference _clickedCity;

    private void Start()
    {
        _tapController.OnOneTap += Open;
        Activate();
    }

    private void Open(ClickableObject clickableObject)
    {
        if(clickableObject.TryGetComponent(out CityNameReference city))
        {
            Activate();
            if (gameObject.activeSelf)
            {
                _clickedCity = city;
                Initialize();
            }
        }
    }

    public void Activate()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        var cityName = root.Q<Label>("CityName");
        cityName.text = _clickedCity.CityName.ToString();
        cityName.style.color = Color.white;

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

    private Color GetColorFromSprite(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        int centerX = texture.width / 2;
        int centerY = texture.height / 2;

        Color pixel = texture.GetPixel(centerX, centerY);

        return pixel;
    }
}
