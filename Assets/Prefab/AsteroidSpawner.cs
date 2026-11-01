using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public float spawnTime = 2f;
    public float spawnX = 8f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        float randomX = Random.Range(-spawnX, spawnX);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
