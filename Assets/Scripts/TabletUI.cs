using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    private List<FactorySpawnable> objectPool = new List<FactorySpawnable>();

    FactoryScriptableObject _selectedFood;
    public void Next()
    {
        _selectedFood = FoodCameraManager.Instance.OnNext();
    }

    public void Previous()
    {
        _selectedFood = FoodCameraManager.Instance.OnPrevious();
    }

    public void OnFoodSelected()
    {
        if (_selectedFood == null)
            return;

        GameManager gm = GameManager.Instance;

        if (_selectedFood as FoodData != null)
        {
            FoodData foodData = _selectedFood as FoodData;
            if (objectPool.Any(factorySpawnable => factorySpawnable.GetType() == foodData.FoodPrefab.GetType()))
            {
                var food = objectPool.FirstOrDefault(factorySpawnable => factorySpawnable.GetType() == foodData.FoodPrefab.GetType());
                objectPool.Remove(food);
                food.gameObject.SetActive(true);
                SpawnFoodBase(gm, food as FoodBase);
                return;
            }
        }
        else if (_selectedFood as TrayData != null)
        {
            TrayData trayData = _selectedFood as TrayData;
            if (objectPool.Any(factorySpawnable => factorySpawnable.GetType() == trayData.TrayPrefab.GetType()))
            {
                var tray = objectPool.FirstOrDefault(factorySpawnable => factorySpawnable.GetType() == trayData.TrayPrefab.GetType());
                objectPool.Remove(tray);
                tray.gameObject.SetActive(true);
                SpawnTrayBase(gm, tray as TrayBase);
                return;
            }
        }

        IFood newFood = gm.FoodFactory.Create(_selectedFood as FoodData);
        if (newFood != null)
        {
            InitializeFoodBase(gm, newFood);
            return;
        }

        ITray newTray = gm.FoodFactory.Create(_selectedFood as TrayData);
        if (newTray != null)
        {
            InitializeTrayBase(gm, newTray);
            return;
        }
    }

    private void InitializeFoodBase(GameManager gm, IFood newFood)
    {
        FoodBase foodBase = newFood as FoodBase;

        SpawnFoodBase(gm, foodBase);
        foodBase.onObjectClose += () => objectPool.Add(foodBase);
    }

    private void SpawnFoodBase(GameManager gm, FoodBase foodBase)
    {
        foodBase.transform.SetPositionAndRotation(gm.FoodSpawnPoint.position, gm.FoodSpawnPoint.rotation);

        foodBase.Serve();

    }

    private void InitializeTrayBase(GameManager gm, ITray newTray)
    {
        TrayBase trayBase = newTray as TrayBase;

        SpawnTrayBase(gm, trayBase);
        trayBase.onObjectClose += () => objectPool.Add(trayBase);
    }

    private void SpawnTrayBase(GameManager gm, TrayBase trayBase)
    {
        trayBase.transform.SetPositionAndRotation(gm.FoodSpawnPoint.position, gm.FoodSpawnPoint.rotation);

        trayBase.Serve();

    }
}
