using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float spawnTime = 2f;
    public float spawnRadius = 12f;
    public int maxSpawn = 4;
    public int minSpawn = 2;
    public Transform player;
    private float timer = 0f;

    void Update()
    {

        if (player != null)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                Spawn();
                timer = 0f;
            }
        }
    }

    void Spawn()
    {
        int spawnCount = Random.Range(minSpawn, maxSpawn + 1);
        for(int i = 0; i < spawnCount; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = (Vector2)player.position + (randomDirection * spawnRadius);
            Instantiate(Prefab, spawnPosition, Quaternion.identity);
        }
    }
}
