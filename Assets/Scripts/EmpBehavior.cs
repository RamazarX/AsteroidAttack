using UnityEngine;

public class EmpBehavior : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 6f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * Random.Range(minSpeed, maxSpeed);
        }
        Destroy(gameObject, 15f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null && player.empCharges < 3)
            {
                player.AddEmp(1); 
            }
            Destroy(gameObject);
        }
    }
}
