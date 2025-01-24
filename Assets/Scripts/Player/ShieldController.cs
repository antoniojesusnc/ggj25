using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static ShieldController Instance { get; private set; }

    public Transform player; // Referencia al jugador (personaje principal)

    private void Awake()
    {
        // Configurar Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destruir el objeto si ya existe una instancia
            return;
        }
        Instance = this;
    }

    void Update()
    {
        // Obtén la posición del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegúrate de que esté en 2D (sin profundidad)

        // Calcula la dirección del escudo hacia el mouse
        Vector3 direction = (mousePosition - player.position).normalized;

        // Calcula el ángulo de rotación basado en la dirección
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        // Aplica la rotación al escudo
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}