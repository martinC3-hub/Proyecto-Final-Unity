using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Parámetros")]
    public float distanciaDeteccion = 40f;
    public float velocidadCaminata = 3f;
    public float velocidadCorrida = 7f;

    private int rutina;
    private float cronometro;
    private float grado;
    private Quaternion angulo;

    [Header("Referencias")]
    public Animator ani;
    public GameObject target;

    void Start()
    {
        if (ani == null)
            ani = GetComponent<Animator>();
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }

    void Comportamiento_Enemigo()
    {
        float distancia = Vector3.Distance(transform.position, target.transform.position);

        if (distancia > distanciaDeteccion)
        {
            Patrullar();
        }
        else
        {
            Perseguir();
        }
    }

    // ---------- PATRULLA ----------
    void Patrullar()
    {
        ani.SetBool("run", false);

        cronometro += Time.deltaTime;
        if (cronometro >= 3)
        {
            rutina = Random.Range(0, 3);
            cronometro = 0;
        }

        switch (rutina)
        {
            case 0:
                ani.SetBool("walk", false);
                break;

            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;

            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 40 * Time.deltaTime);
                transform.Translate(Vector3.forward * velocidadCaminata * Time.deltaTime);
                ani.SetBool("walk", true);
                break;
        }
    }

    // ---------- PERSEGUIR ----------
    void Perseguir()
    {
        Vector3 direccion = (target.transform.position - transform.position);
        direccion.y = 0;

        Quaternion rot = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 150f * Time.deltaTime);

        ani.SetBool("walk", false);
        ani.SetBool("run", true);

        transform.Translate(Vector3.forward * velocidadCorrida * Time.deltaTime);
    }
}
