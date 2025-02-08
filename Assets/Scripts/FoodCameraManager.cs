using DesignPatterns.Generics;
using System.Collections.Generic;
using UnityEngine;

public class FoodCameraManager : Singleton<FoodCameraManager>
{
    [SerializeField] List<FoodBase> foodList;
    [SerializeField] List<FoodData> foodData;
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

    public FoodData OnNext()
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

    public FoodData OnPrevious()
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
