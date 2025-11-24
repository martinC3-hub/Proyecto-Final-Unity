using UnityEngine;

public class BillboardY : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 look = Camera.main.transform.position;
        look.y = transform.position.y; // solo rota en eje Y
        transform.LookAt(look);
    }
}
