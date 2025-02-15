using UnityEngine;
[CreateAssetMenu(fileName ="NewFoodData", menuName ="ScriptableObjects/FoodData")]
public class FoodData : FactoryScriptableObject, ISpawnable
{
    public string Name;
    public float Price;
    public float PreparationTime;
    public float TimeUse;
    public FoodBase FoodPrefab;
    public GameObject GetGameObject()
    {
        return null;
    }
}
