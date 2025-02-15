using UnityEngine;

public interface IFactory
{
    IFood Create(FoodData foodData);
    ITray Create(TrayData trayData);
}
