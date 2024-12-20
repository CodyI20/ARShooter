using UnityEngine;


/// <summary>
/// This class is responsible for spawning enemies in the game. It will spawn enemies at a random position within a certain range.
/// It will also check if there are any other objects in the spawn area, and if there are, it will not spawn the enemy.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public static event System.Action OnEnemySpawn;
    [SerializeField, Tooltip("The enemy prefabs to spawn.")] private GameObject[] enemyPrefabs;
    [SerializeField, Tooltip("The spawn rate of the enemies.")] private float spawnRate = 1.0f;
    [SerializeField, Tooltip("The area in which the enemies can spawn.")] private float spawnArea = 1.0f;
    private float spawnTimer = 0.0f;
    private bool searchingForSpawnPosition = false;

    //Not mandatory, but it's a good practice to draw a gizmo to visualize the spawn area
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnArea);
    }

    //Checks if the enemy can spawn on the position
    bool CanSpawnEnemyOnPosition(Vector3 spawnPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1.0f);

        if (colliders.Length > 0)
        {
            for(int i=colliders.Length-1; i>=0; i--)
            {
                if (i > colliders.Length - 1)
                    continue;
                else if (colliders[i].gameObject.CompareTag("Enemy"))
                    return false;
            }
        }

        return true;
    }

    //Method that will spawn the enemies with the spawn rate
    private void SpawnEnemy()
    {
        if (spawnTimer <= 0)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnArea, spawnArea), -1, Random.Range(-spawnArea, spawnArea));
            while(!CanSpawnEnemyOnPosition(randomSpawnPosition))
            {
                searchingForSpawnPosition = true;
                randomSpawnPosition = new Vector3(Random.Range(-spawnArea, spawnArea), -1, Random.Range(-spawnArea, spawnArea));
            }
            searchingForSpawnPosition = false;
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], randomSpawnPosition, Quaternion.identity);
            OnEnemySpawn?.Invoke();
            spawnTimer = spawnRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
        if (searchingForSpawnPosition == false && spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
