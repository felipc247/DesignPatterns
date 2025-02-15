using UnityEngine;

public interface ITray : ISpawnable
{
    void Serve();
    float GetPrice();
    float GetPreparationTime();
    float TimeUse();
    Vector2 GetDimension();
}