using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float spawnTime = 2f;
    public float spawnRadius = 12f;
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
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)player.position + (randomDirection * spawnRadius);
        Instantiate(Prefab, spawnPosition, Quaternion.identity);
    }
}
