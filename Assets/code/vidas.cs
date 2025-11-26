using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class vidas : MonoBehaviour
{
    public int vidaPlayer = 10;
    public Slider vidaVisual;

    void Update()
    {
        vidaVisual.value = vidaPlayer;

        if (vidaPlayer <= 0)
        {
            SceneManager.LoadScene("Derrota");
            Debug.Log("Perdiste!!!");
        }
    }
}
