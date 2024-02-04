using System;
using UnityEngine;

//This class is responsible for the enemy behavior, not including the movement
public class Enemy : MonoBehaviour
{
    public static event Action<int> OnEnemyDestroyed;

    [SerializeField, Tooltip("The health of the enemy.")] private int health = 100;
    [SerializeField, Tooltip("The score given when the enemy is destroyed.")] private int score = 10;

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
        if (health <= 0)
        {
            OnEnemyDestroyed?.Invoke(score);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke(score);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
