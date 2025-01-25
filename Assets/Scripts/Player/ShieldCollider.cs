using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj25
{
    public class ShieldCollider : MonoBehaviour
    {
        // Números de las layers
        private const int LAYER_SPIKE = 8;

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Detectar colisión con layers específicas
            int collisionLayer = collision.gameObject.layer;

            if (collisionLayer == LAYER_SPIKE)
            {
                Debug.Log("Escudo bloqueó un proyectil: " + collision.gameObject.name);
                Destroy(collision.gameObject); // Destruir proyectil al impactar
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Detectar colisión con layers específicas
            int otherLayer = other.gameObject.layer;

            if (otherLayer == LAYER_SPIKE)
            {
                Debug.Log("Escudo bloqueó un proyectil (Trigger): " + other.gameObject.name);
                Destroy(other.gameObject); // Destruir proyectil al impactar
            }
        }
    }
}
