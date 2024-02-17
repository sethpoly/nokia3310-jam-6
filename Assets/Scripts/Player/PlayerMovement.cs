using UnityEngine;

public class PlayerMovement: MonoBehaviour 
{
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float slowDownMultiplier = .3f;
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector2(horizontal, vertical);
        move = horizontalSpeed * Time.fixedDeltaTime * move.normalized;
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
}