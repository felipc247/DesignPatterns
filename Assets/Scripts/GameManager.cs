using DesignPatterns.Generics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform FoodSpawnPoint;
    public IFoodFactory FoodFactory { get; private set; }

    public override void Awake()
    {
        base.Awake();
        FoodFactory = new Factory();

        var spawnPoint = FindFirstObjectByType<SpawnPoint>();
        if (spawnPoint != null)
        {
            FoodSpawnPoint = spawnPoint.transform;
        }
    }
}
