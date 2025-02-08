using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private IFoodFactory foodFactory;

    private void Awake()
    {
        foodFactory = new Factory();
    }

    public void OnFoodSelected(FoodData foodData)
    {
        // creiamo l'oggetto
        IFood newFood = foodFactory.Create(foodData);
        FoodBase foodBase = newFood as FoodBase;

        foodBase.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation); // TODO: da cambiare

        foodBase.Serve();
    }
}
