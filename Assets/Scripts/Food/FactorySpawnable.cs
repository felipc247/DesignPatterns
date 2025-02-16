using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FactorySpawnable : MonoBehaviour
{
    public Action onObjectClose;
    public bool DestroyOnAwake;
    public virtual IEnumerator CloseGameObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        onObjectClose?.Invoke();
    }
}