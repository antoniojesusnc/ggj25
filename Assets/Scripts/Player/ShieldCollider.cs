using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj25
{
    public class ShieldCollider : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            // Escudo detecta colisión con proyectiles
            if (collision.gameObject.CompareTag("Projectile"))
            {
                Debug.Log("Escudo bloqueó el proyectil: " + collision.gameObject.name);
                Destroy(collision.gameObject); // Destruir proyectil al impactar
            }
        }

        // Detectar triggers (si el escudo está configurado como Trigger)
        void OnTriggerEnter2D(Collider2D other)
        {
            // Escudo detecta colisión con proyectiles
            if (other.gameObject.CompareTag("Projectile"))
            {
                Debug.Log("Escudo bloqueó un proyectil (Trigger): " + other.gameObject.name);
                Destroy(other.gameObject); // Destruir proyectil al impactar
            }
        }
    }
}
