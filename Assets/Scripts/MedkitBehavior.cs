using UnityEngine;

public class MedkitBehavior : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            rb.linearVelocity = direction * randomSpeed;
        }
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null && playerController.health < 100)
            {
                playerController.TakeDamage(-20);
            }
        }
        Destroy(gameObject);
    }
}
