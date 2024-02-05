using UnityEngine;


/// <summary>
/// This class is responsible for the audio management. It holds all the audio clips and plays them when needed using the Observer pattern.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("The audio source for the gun shot")] private AudioSource gunShotAudioSource;
    [SerializeField, Tooltip("The audio source for the enemy hit")] private AudioSource enemyHitAudioSource;
    [SerializeField, Tooltip("The audio source for when the enemy dies")] private AudioSource enemyDeathAudioSource;

    private void Awake()
    {
        //Doing null checks for the audio sources
        if (gunShotAudioSource == null)
        {
            Debug.LogError("The gun shot audio source is not set in the AudioManager");
        }
        if (enemyHitAudioSource == null)
        {
            Debug.LogError("The enemy hit audio source is not set in the AudioManager");
        }
        if (enemyDeathAudioSource == null)
        {
            Debug.LogError("The enemy death audio source is not set in the AudioManager");
        }
        //
    }
    private void OnEnable()
    {
        //Gun event subscription
        Gun.OnShoot += PlayGunShot;

        //Enemy event subscription
        Enemy.OnEnemyHit += PlayEnemyHit;
        Enemy.OnEnemyDeath += PlayEnemyDeath;
    }
    private void OnDisable()
    {
        //Gun event unsubscription
        Gun.OnShoot -= PlayGunShot;

        //Enemy event unsubscription
        Enemy.OnEnemyHit -= PlayEnemyHit;
        Enemy.OnEnemyDeath -= PlayEnemyDeath;
    }

    //Gun sounds
    public void PlayGunShot()
    {
        gunShotAudioSource.Play();
    }

    //Enemy sounds
    public void PlayEnemyHit(int damage)
    {
        enemyHitAudioSource.Play();
    }
    public void PlayEnemyDeath(int score)
    {
        enemyDeathAudioSource.Play();
    }
}
