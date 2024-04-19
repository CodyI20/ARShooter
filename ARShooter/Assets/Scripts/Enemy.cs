using System;
using System.Collections;
using UnityEngine;

//This class is responsible for the enemy behavior, not including the movement
public class Enemy : MonoBehaviour
{
    public static event Action<int> OnEnemyDeath;
    public static event Action<int> OnEnemyHit;

    [SerializeField, Tooltip("The health of the enemy.")] private int health = 100;
    [SerializeField, Tooltip("The score given when the enemy is destroyed.")] private int score = 10;
    [SerializeField, Tooltip("The amount of time in seconds the enemy will be in the scene before being destroyed.")] private float timeToLive = 10.0f;

    private void OnEnable()
    {
        Bullet.OnBulletHit += HandleEnemyTakeDamage;
    }

    private void OnDisable()
    {
        Bullet.OnBulletHit -= HandleEnemyTakeDamage;
    }

    void HandleEnemyTakeDamage(int damage, Enemy enemy)
    {
        if(enemy != this) return;
        health -= damage;
        OnEnemyHit?.Invoke(damage);
        if (health <= 0)
        {
            OnEnemyDeath?.Invoke(score);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

}
