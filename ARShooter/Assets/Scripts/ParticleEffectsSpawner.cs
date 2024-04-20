using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("The particle effect list to spawn from when the bullet is destroyed.")] private List<ParticleSystem> particleEffect;
    private void OnEnable()
    {
        Bullet.OnBulletDestroyed += SpawnParticleEffect;
    }
    private void OnDisable()
    {
        Bullet.OnBulletDestroyed -= SpawnParticleEffect;
    }

    private ParticleSystem getRandomParticleEffect()
    {
        return particleEffect[Random.Range(0, particleEffect.Count)];
    }

    private void SpawnParticleEffect(Transform transform)
    {
        ParticleSystem particle = getRandomParticleEffect();
        Instantiate(particle, transform.position, Quaternion.identity);

        if(particle.main.stopAction != ParticleSystemStopAction.Destroy)
        {
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}
