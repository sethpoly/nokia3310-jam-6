using UnityEngine;

public class PlayerMovement: MonoBehaviour 
{
    [SerializeField] private float fallSpeed = 50f;
    [SerializeField] private float horizontalSpeed = 100f;
    private float currentSlowDownMultiplier = 0f;
    private Rigidbody2D rb;

    private float horizontal;
    private readonly float vertical = -1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector2(horizontal * horizontalSpeed, GetFallSpeed());
        move = Time.fixedDeltaTime * move;
        rb.velocity = move;
    }

    void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private float GetFallSpeed()
    {
        float multipler = currentSlowDownMultiplier + 1;
        return vertical * fallSpeed / multipler;
    }

    public void SetSlowdownMultiplier(float multiplier)
    {
        currentSlowDownMultiplier = multiplier;
    }
}