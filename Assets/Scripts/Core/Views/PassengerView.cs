using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassengerView : MonoBehaviour
{
    public PassengerScheme Passenger { get; private set; }
    public CityView CityNameRefSpawn { get; private set; }
    public TrainView Train { get; set; }
    public Sprite CitySprite { get; private set; }

    /// <summary>
    /// Contsructor
    /// </summary>
    /// <param name="passenger"></param>
    /// <param name="citySpawn"></param>
    public void Construct(PassengerScheme passenger, CityView citySpawn)
    {
        Passenger = passenger;
        CityNameRefSpawn = citySpawn;
    }

    private void Start()
    {
        _ = OnStart();
    }

    private async UniTaskVoid OnStart()
    {
        CitySprite = await CityService.LoadCitySpite(Passenger.CityTo);
        CityNameRefSpawn = CityService.GetCityView(Passenger.CitySpawn);

        //Set sprite
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CitySprite;
        spriteRenderer.sortingOrder = 2;

        PassengerService.SetPassengerTransform(CityNameRefSpawn, this);
    }
}
 