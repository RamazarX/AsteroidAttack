using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float speed = 20.0f;
    public float rotationSpeed = 10f;
    public Rigidbody2D rb;
    public PlayerControl controls;
    public Vector2 movementInput;
    public int health = 100;
    public int score = 0;
    public int empCharges = 0;
    public float empRadius = 8f;
    public GameObject empWavePrefab;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI empChargeText;

    void Start()
    {
        UpdateUI();
    }

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
        if (movementInput != Vector2.zero)
        {
            transform.up = Vector3.Slerp(transform.up, movementInput, rotationSpeed * Time.deltaTime);
        }

        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            UseEMP();
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
        UpdateUI();
        if (health <= 0)
        {
            Debug.Log("Корабль уничтожен!");
            Destroy(gameObject);
        }
        if (health <= 0)
        {
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void AddEmp(int amount)
    {
        empCharges += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "Жизни: " + health;
        }
        if (scoreText != null)
        {
            scoreText.text = "Очки: " + score;
        }
        if (empChargeText != null)
        {
            empChargeText.text = "ЭМИ: " + empCharges;
        }
    }
    void UseEMP()
    {
        if (empCharges > 0)
        {
            empCharges--;
            UpdateUI();
            Instantiate(empWavePrefab, transform.position, Quaternion.identity);
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, empRadius);
            foreach (Collider2D hit in hitObjects)
            {
                if (hit.GetComponent<AsteroidBehavior>() != null)
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }
}
