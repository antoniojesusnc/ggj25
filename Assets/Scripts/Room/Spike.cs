using System;
using UnityEngine;

namespace ggj25
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] public bool _destroyOnHit;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_destroyOnHit)
            {
                return;
            }
            
            Destroy(gameObject); // Destruir proyectil al impactar
            
            //De momento dejo esta cancion para empezar
            SoundManager.Instance.PlaySFX(AudioType.SFX.ProjectileDestroyed);
        }
    }
}
