using UnityEngine;
[CreateAssetMenu(fileName ="NewFoodData", menuName ="ScriptableObjects/FoodData")]
public class FoodData : ScriptableObject
{
    public string Name;
    public float Price;
    public float PreparationTime;
    public FoodBase FoodPrefab;
}
