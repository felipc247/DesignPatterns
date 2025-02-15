using DesignPatterns.Generics;
using System.Collections.Generic;
using UnityEngine;

public class FoodCameraManager : Singleton<FoodCameraManager>
{
    [SerializeField] List<FactorySpawnable> foodList;
    [SerializeField] List<FactoryScriptableObject> foodData;

    int _currentIndex;

    public override void Awake()
    {
        base.Awake();
        _currentIndex = -1;
        DeactivateAll();
    }

    private void DeactivateAll()
    {
        foreach (var food in foodList)
        {
            food.gameObject.SetActive(false);
        }
    }

    public FactoryScriptableObject OnNext()
    {
        if (foodList.Count == 0) return default;

        if (_currentIndex >= 0 && _currentIndex < foodList.Count)
        {
            foodList[_currentIndex].gameObject.SetActive(false);
        }

        _currentIndex = (_currentIndex + 1) % foodList.Count;
        foodList[_currentIndex].gameObject.SetActive(true);

        return foodData[_currentIndex];
    }

    public FactoryScriptableObject OnPrevious()
    {
        if (foodList.Count == 0) return default;

        if (_currentIndex >= 0 && _currentIndex < foodList.Count)
        {
            foodList[_currentIndex].gameObject.SetActive(false);
        }

        _currentIndex = (_currentIndex - 1 + foodList.Count) % foodList.Count;
        if (_currentIndex < 0) _currentIndex = foodList.Count - 1;

        foodList[_currentIndex].gameObject.SetActive(true);

        return foodData[_currentIndex];
    }
}
