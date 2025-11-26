using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody rigidbody;
    public float rotationSpeed = 270;
    public Animator animator;
    public float velocidad = 12;

    public Rigidbody rb;
    public int damage = 1;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;
    public int valor = 6;


    public float sprintMultiplier = 1.8f;   // velocidad extra al correr
    public float sprintDuration = 3f;       // tiempo máximo corriendo
    public float sprintRecharge = 1f;       // velocidad de recarga
    private float sprintTimer;
    private bool isSprinting;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sprintTimer = sprintDuration;  // comienza lleno
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

       
        if (Input.GetKey(KeyCode.LeftShift) && y > 0.1f && sprintTimer > 0)
        {
            isSprinting = true;
            sprintTimer -= Time.deltaTime;

            //animator.SetBool("Run", true);
        }
        else
        {
            isSprinting = false;
            if (sprintTimer < sprintDuration)
                sprintTimer += Time.deltaTime * sprintRecharge;

            //animator.SetBool("Run", false);
        }

        // aplicar velocidad
        float velocidadActual = velocidad;
        if (isSprinting) velocidadActual *= sprintMultiplier;



        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadActual);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);


        if (Input.GetKey("f"))
        {
            animator.SetBool("other", false);
            animator.Play("baile god");
        }
        if (Input.GetKey("c"))
        {
            animator.SetBool("other", false);
            animator.Play("capoeira");
        }
        if (x != 0 || y != 0)
        {
            animator.SetBool("other", true);
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            animator.Play("jump");
            Invoke("Jump", 0);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            vidas v = GetComponent<vidas>();
            if (v != null)
            {
                v.vidaPlayer -= damage;
            }
        }

        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            GameManager.instance.AgregarPuntos(valor);
            Destroy(collision.gameObject);

            if (valor >= 7)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("victoria");
            }
        }
    }


}
