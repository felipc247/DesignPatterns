using UnityEngine;

public abstract class FoodBase : MonoBehaviour, IFood
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
}
