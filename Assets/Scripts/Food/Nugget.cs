using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Nugget : MonoBehaviour
{
    public delegate void OnCollision();
    public OnCollision onCollisionEnter;
    public Rigidbody Body;
    public int Damage = 1;
    public float KnockbackForce = 2f;
    public float KnockbackHeight = 2f;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke();

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damage);

            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                // get the direction from the nugget to the object
                // and add force in that direction

                Vector3 direction = collision.transform.position - transform.position;

                direction.Normalize();

                // multiply the direction by the nugget's velocity and the knockback force
                direction *= Body.linearVelocity.magnitude * KnockbackForce;

                direction.y = KnockbackHeight;

                rb.AddForce(direction, ForceMode.Impulse);
            }
        }

        gameObject.SetActive(false);
    }
}
