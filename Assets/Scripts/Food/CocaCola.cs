using System;
using System.Collections;
using UnityEngine;

public class CocaCola : FoodBase
{
    public override void Serve()
    {
        if (DestroyOnAwake)
            StartCoroutine(CloseGameObject(TimeUse()));
    }
}
