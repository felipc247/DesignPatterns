using UnityEngine;

public class Hamburger : FoodBase
{
    public override void Serve()
    {
        if (DestroyOnAwake)
            StartCoroutine(CloseGameObject(TimeUse()));
    }
}
