using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassengerAttached : MonoBehaviour
{
    public Passenger Passenger { get; set; }
    public Train Train { get; set; }
    public Sprite CitySprite { get; private set; }

    public void Construct(Passenger passenger, Train train)
    {
        Passenger = passenger;
        Train = train;
    }

    void Start()
    {
        CitySprite = CityService.LoadCitySpite(Passenger.CityTo);

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CitySprite;
        spriteRenderer.sortingOrder = 2;

        PassengerService.SetPassengerTransform(Train, this);
    }
}
 