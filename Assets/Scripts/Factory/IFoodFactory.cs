using UnityEngine;

public interface IFoodFactory
{
    IFood Create(FoodData foodData);
}
