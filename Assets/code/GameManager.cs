using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int totalColeccionables;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Cuenta todos los coleccionables en la escena
        totalColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length;

        ActualizarUI();
    }

    public void AgregarPuntos(int puntos)
    {
        score += puntos;
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        scoreText.text = score + " / " + totalColeccionables;
    }
}
