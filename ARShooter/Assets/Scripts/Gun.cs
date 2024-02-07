using System;
using UnityEngine;

/// <summary>
/// This class is responsible for the gun behaviour
/// </summary>
public class Gun : MonoBehaviour
{
    public static event Action OnShoot;

    [SerializeField, Tooltip("The bullet prefab")] private GameObject bulletPrefab;
    [SerializeField, Tooltip("The bullet spawn point")] private Transform bulletSpawnPoint;
    [SerializeField, Tooltip("The fire rate of the gun")] private float fireRate = 0.5f;

    private float fireTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Shoot()
    {
        if(fireTimer > 0)
        {
            return;
        }
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        OnShoot?.Invoke();
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Shoot();
        }
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
            if(fireTimer < 0) fireTimer = 0;
        }
    }
}
