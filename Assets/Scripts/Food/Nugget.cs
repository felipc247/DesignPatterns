using System;
using UnityEngine;
using UnityEngine.Events;

public class Nugget : MonoBehaviour
{
    public delegate void OnCollision();
    public OnCollision onCollisionEnter;
    public Rigidbody Body;
    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke();
        gameObject.SetActive(false);
    }
}
