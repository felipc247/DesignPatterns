using UnityEngine;

public class BurgerMenu : TrayBase
{
    public override void Serve()
    {
        if (DestroyOnAwake)
            StartCoroutine(CloseGameObject(TimeUse()));
    }
}
