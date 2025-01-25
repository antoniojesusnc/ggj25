using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static ShieldController Instance { get; private set; }

    [SerializeField] private Transform shield; // Referencia al jugador (personaje principal)
    private float shieldDistance = 1.5f;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegúrate de que esté en 2D (sin profundidad)

        // Calcula la dirección desde la posición del jugador hacia el mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calcula el ángulo de rotación basado en la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calcula la nueva posición del escudo en base al ángulo y la distancia
        Vector3 shieldPosition = transform.position + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * shieldDistance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * shieldDistance,
            0
        );
        // Actualiza la posición del escudo

        // Aplica la rotación para que el escudo apunte hacia el ratón
        shield.SetPositionAndRotation(shieldPosition, Quaternion.Euler(0, 0, angle));

    }

    //player collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Escudo detecta colisión con proyectiles
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Player bloqueó el proyectil: " + collision.gameObject.name);
            //Destroy(collision.gameObject); // Destruir proyectil al impactar
        }
    }

    //player trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar colisiones con triggers
        if (other.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Trigger en el escudo: " + other.gameObject.name);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger en el jugador: " + other.gameObject.name);
        }
        else
        {
            Debug.Log("Trigger desconocido: " + other.gameObject.name);
        }
    }



}