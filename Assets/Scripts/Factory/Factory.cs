using UnityEngine;
using UnityEngine.UI;

public class Factory : IFactory
{
    public IFood Create(FoodData foodData)
    {
        try
        {
            FoodBase newFoodBase = FoodBase.Instantiate(foodData.FoodPrefab); // posso chiamare Instantiate perché FoodBase è MonoBehavior

            if (newFoodBase == null || newFoodBase is not IFood) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {foodData.Name}");
                return default;
            }

            newFoodBase.Initialize(foodData);

            return newFoodBase;
        }
        catch
        {
            return default;
        }
    }

    public ITray Create(TrayData trayData)
    {
        try
        {
            TrayBase newTrayBase = TrayBase.Instantiate(trayData.TrayPrefab);

            if (newTrayBase == null || newTrayBase is not ITray)
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {trayData.Name}");
                return default;
            }

            newTrayBase.Initialize(trayData);
            return newTrayBase;
        }
        catch
        {
            return default;
        }
    }
}
