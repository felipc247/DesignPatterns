using UnityEngine;

public abstract class FoodBase : FactorySpawnable, IFood
{
    protected FoodData foodData;

    public virtual void Initialize(FoodData foodData)
    {
        this.foodData = foodData; 
    }

    public float GetPreparationTime()
    {
        return foodData.PreparationTime;
    }

    public float GetPrice()
    {
        return foodData.Price;
    }

    public abstract void Serve(); // questo viene implementato da chi estende FoodBase

    public float TimeUse()
    {
        return foodData.TimeUse;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
