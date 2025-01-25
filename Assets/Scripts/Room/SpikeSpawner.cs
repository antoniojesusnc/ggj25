using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [Header("Configuración del disparador")]
    [SerializeField] private GameObject spikePrefab; // Prefab del pincho
    [SerializeField] private Transform shootPoint;   // Punto de disparo
    [SerializeField] private float shootFrequency = 2f; // Frecuencia de disparo (en segundos)
    [SerializeField] private Vector2 shootDirection = Vector2.right; // Dirección de disparo
    [SerializeField] private float spikeSpeed = 5f; // Velocidad del pincho

    private float timer;

    private void Update()
    {
        // Actualiza el temporizador
        timer += Time.deltaTime;

        // Dispara un pincho si se cumple el tiempo de frecuencia
        if (timer >= shootFrequency)
        {
            ShootSpike();
            timer = 0f; // Reinicia el temporizador
        }
    }

    private void ShootSpike()
    {
        // Instancia el pincho en el punto de disparo
        GameObject spike = Instantiate(spikePrefab, shootPoint.position, Quaternion.identity);

        // Ajusta la dirección y velocidad del pincho
        Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootDirection.normalized * spikeSpeed;
        }

        // Opcional: destruye el pincho después de un tiempo para optimizar
        Destroy(spike, 5f); // Elimina el pincho después de 5 segundos
    }

    // Función para cambiar la dirección del disparo desde otros scripts
    public void SetShootDirection(Vector2 newDirection)
    {
        shootDirection = newDirection;
    }
}
