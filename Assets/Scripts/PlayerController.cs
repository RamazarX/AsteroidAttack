using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Rigidbody2D rb;
    public PlayerControl controls;
    public Vector2 movementInput;
    public int health = 100;

    // initialize the controls
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControl();
        if(Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput=controls.Gameplay.Move.ReadValue<Vector2>();
        if (movementInput == Vector2.zero && Accelerometer.current != null)
        {
            Vector3 v3 = Accelerometer.current.acceleration.ReadValue();
            movementInput = new Vector2(v3.x, v3.y);
        }
    }

    // Update is called once per second
    void FixedUpdate()
    {
        rb.AddForce(movementInput * speed, ForceMode2D.Force);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Ай! Осталось жизней: " + health);
        if (health <= 0)
        {
            Debug.Log("Корабль уничтожен!");
            Destroy(gameObject);
        }
    }

}
