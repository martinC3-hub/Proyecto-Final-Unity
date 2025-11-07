using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Componentes")]
    private Rigidbody rb;
    public Animator animator;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    [Header("Salto")]
    public float jumpForce = 6f;
    public LayerMask groundMask;   // Capa del suelo
    public Transform groundCheck;  // Un Empty en los pies del personaje
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

    private float horizontal;
    private float vertical;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- Captura de Input ---
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpPressed = Input.GetButtonDown("Jump"); // Por defecto: tecla Espacio

        // --- Parámetros del Animator ---
        animator.SetFloat("VelX", horizontal);
        animator.SetFloat("VelY", vertical);
        animator.SetBool("isGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // --- Comprobar si está en el suelo ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // --- Movimiento ---
        Vector3 move = transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // --- Rotación ---
        if (horizontal != 0)
        {
            Quaternion turn = Quaternion.Euler(0f, horizontal * rotationSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turn);
        }

        // --- Salto ---
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el círculo del GroundCheck en el editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
