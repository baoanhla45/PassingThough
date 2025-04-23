using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public int maxMana = 3;
    public int currentMana;

    public int money = 0;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private bool jumpInput;
    private bool isGrounded;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    private void OnEnable()
    {
        inputActions.Enable();

        // Gán callback cho Input
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Attack.performed += ctx => Attack();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Tính toán velocity
        Vector2 velocity = rb.velocity;
        velocity.x = moveInput.x * moveSpeed;

        if (isGrounded && jumpInput)
        {
            velocity.y = jumpForce;
            jumpInput = false;
        }

        rb.velocity = velocity;
    }
    private void Jump()
    {
        jumpInput = true;      
    }

    private void Attack()
    {
        Debug.Log("Attack triggered!");
        // Sau này bạn có thể chơi animation, kiểm tra kẻ địch...
    }
    public void LoadPlayerData(int health, int mana, int money)
    {
        currentHealth = health;
        currentMana = mana;
        this.money = money;
    }
}
