using UnityEngine;
using UnityEngine.UI;


public abstract class TrayBase : FactorySpawnable, ITray
{
    protected TrayData trayData;
    private void OnEnable()
    {
        if (DestroyOnAwake && trayData != null)
            Destroy(gameObject, TimeUse());
    }

    private void Start()
    {
        if (DestroyOnAwake)
            Destroy(gameObject, TimeUse());
    }
    public virtual void Initialize(TrayData trayData)
    {
        this.trayData = trayData;
    }

    public float GetPreparationTime()
    {
        return trayData.PreparationTime;
    }

    public float GetPrice()
    {
        return trayData.Price;
    }

    public abstract void Serve();

    public float TimeUse()
    {
        return trayData.TimeUse;
    }

    public Vector2 GetDimension()
    {
        return trayData.Dimension;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

