using UnityEngine;

public class TabletUI : MonoBehaviour
{
    FoodData _selectedFood;
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

        IFood newFood = gm.FoodFactory.Create(_selectedFood);
        FoodBase foodBase = newFood as FoodBase;

        foodBase.transform.SetPositionAndRotation(gm.FoodSpawnPoint.position, gm.FoodSpawnPoint.rotation); // TODO: da cambiare

        foodBase.Serve();
    }
}
