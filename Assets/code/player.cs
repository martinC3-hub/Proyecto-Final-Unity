using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody rigidbody;
    public float rotationSpeed = 270;
    public Animator animator;
    public float velocidad = 12;

    public Rigidbody rb;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidad);

        //Vector3 movimiento = new Vector3(x, 0, y);
        //rigidbody.MovePosition(rigidbody.position + movimiento.normalized*0.5f);
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
        if (x>0 || x<0 || y>0 || y < 0)
        {
            animator.SetBool("other", true);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey("space") && isGrounded)
        {
            animator.Play("jump");
            //Invoke("Jump", 1f);
            Invoke("Jump", 0);
        }

    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
