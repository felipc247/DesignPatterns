using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrayData", menuName = "ScriptableObjects/TrayData")]
public class TrayData : FactoryScriptableObject, ISpawnable
{
    public string Name;
    public float Price;
    public float PreparationTime;
    public float TimeUse;
    public Vector2 Dimension;
    public TrayBase TrayPrefab;
    public GameObject GetGameObject()
    {
        return null;
    }
}