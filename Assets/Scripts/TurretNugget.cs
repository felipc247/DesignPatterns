using System;
using System.Collections;
using UnityEngine;

public class TurretNugget : MonoBehaviour
{
    ObjectPooler<Nugget> nuggetPooler;
    [SerializeField] Nugget nuggetPrefab;
    [SerializeField] float timeShoot;
    [SerializeField] Transform shootPivot;
    [SerializeField] float shootPower;
    private void Awake()
    {
        nuggetPooler = new ObjectPooler<Nugget>(nuggetPrefab);
    }

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeShoot);
            Shoot();
        }
    }

    private void Shoot()
    {
        Nugget spawnedNugget = nuggetPooler.Get();

        if (!spawnedNugget.gameObject.activeSelf)
        {
            spawnedNugget.gameObject.SetActive(true);
        }
        else
        {
            spawnedNugget.onCollisionEnter += () =>
            {
                nuggetPooler.Set(spawnedNugget);
            };
        }

        spawnedNugget.transform.SetPositionAndRotation(shootPivot.position, Quaternion.identity);
        spawnedNugget.Body.linearVelocity = Vector3.zero;
        spawnedNugget.Body.AddForce(shootPivot.forward * shootPower, ForceMode.Impulse);
    }
}