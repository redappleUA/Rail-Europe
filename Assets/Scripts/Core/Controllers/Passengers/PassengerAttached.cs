using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassengerAttached : MonoBehaviour
{
    public Passenger Passenger { get; private set; }
    public CityNameReference CityNameRefSpawn { get; private set; }
    public Train Train { get; set; }
    public Sprite CitySprite { get; private set; }

    public void Construct(Passenger passenger, CityNameReference citySpawn)
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
        CityNameRefSpawn = CityService.GetCityNameReference(Passenger.CitySpawn);

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CitySprite;
        spriteRenderer.sortingOrder = 2;

        PassengerService.SetPassengerTransform(CityNameRefSpawn, this);
    }
}
 