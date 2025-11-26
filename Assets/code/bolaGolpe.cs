using UnityEngine;

public class bolaGolpe : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vidas v = other.GetComponent<vidas>();
            if (v != null)
            {
                v.vidaPlayer -= damage;
            }
        }
    }
}
