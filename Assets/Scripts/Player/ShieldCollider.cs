using UnityEngine;

namespace ggj25
{
    public class ShieldCollider : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _shieldHitEffect;

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

            HitInShield();
            
            if (otherLayer == LAYER_SPIKE)
            {
                DestroyProjectile(other);
            }
        }

        private void HitInShield()
        {
            SoundManager.Instance.PlaySFX(AudioType.SFX.HitInShield);
            if (!_shieldHitEffect.gameObject.activeSelf)
            {
                _shieldHitEffect.gameObject.SetActive(true);
            }

            _shieldHitEffect.Play();
        }

        private static void DestroyProjectile(Collider2D other)
        {
            
            Debug.Log("Escudo bloqueó un proyectil (Trigger): " + other.gameObject.name);
            Destroy(other.gameObject); // Destruir proyectil al impactar
            
            //De momento dejo esta cancion para empezar
            SoundManager.Instance.PlaySFX(AudioType.SFX.ProjectileDestroyed);
        }
    }
}
