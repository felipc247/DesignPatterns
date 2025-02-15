using UnityEngine;

public class TabletUI : MonoBehaviour
{
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

    private static void InitializeFoodBase(GameManager gm, IFood newFood)
    {
        FoodBase foodBase = newFood as FoodBase;

        foodBase.transform.SetPositionAndRotation(gm.FoodSpawnPoint.position, gm.FoodSpawnPoint.rotation);

        foodBase.Serve();
    }

    private static void InitializeTrayBase(GameManager gm, ITray newTray)
    {
        TrayBase trayBase = newTray as TrayBase;

        trayBase.transform.SetPositionAndRotation(gm.FoodSpawnPoint.position, gm.FoodSpawnPoint.rotation);

        trayBase.Serve();
    }
}
