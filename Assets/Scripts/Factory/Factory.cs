using UnityEngine;

public class Factory : IFoodFactory
{
    public IFood Create(FoodData foodData)
    {
        FoodBase newFoodBase = FoodBase.Instantiate(foodData.FoodPrefab); // posso chiamare Instantiate perché FoodBase è MonoBehavior

        if (newFoodBase == null || newFoodBase is not IFood) //  typeof(newFoodBase.GetType()) != IFood
        {
            Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {foodData.Name}");
            return default;
        }

        newFoodBase.SetData(foodData);

        return newFoodBase;
    }
}
