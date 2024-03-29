using System;
using UnityEngine;

//This class is responsible for the bullet travel behavior
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    //Event for when the bullet hits something
    public static event Action<int, Enemy> OnBulletHit;

    //Bullet Properties
    [SerializeField, Tooltip("The speed of the bullet.")] private float speed = 1.0f;
    [SerializeField, Tooltip("The damage of the bullet.")] private int damage = 10;

    //Bullet extra settings
    [SerializeField, Tooltip("Time to destroy the bullet.")] private float timeToDestroy = 2.5f;
    //Rigidbody component
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            OnBulletHit?.Invoke(damage, other.gameObject.GetComponent<Enemy>());
        }
        else
        {
            OnBulletHit?.Invoke(0, null);
        }
        Destroy(gameObject);
    }
}
