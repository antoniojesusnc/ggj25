using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj25
{
    public class EnemyBase : SpikeSpawner
    {
        [SerializeField] private float moveSpeed = 3f;
        private GameObject currentEnemy;
        private RoomController _room;

        private GameObject Target;

        private void Start()
        {
            Target = GameObject.Find("Hero");
            _room = GetComponentInParent<RoomController>();
        }

        protected override void ShootSpike()
        {
            if (!_room.IsActive)
            {
                return;
            }
            if (Target != null)
            {
                // Calcula la dirección hacia el héroe
                Vector3 directionToHero = (Target.transform.position - shootPoint.position).normalized;

                // Instancia el pincho en el punto de disparo
                GameObject spike = Instantiate(spikePrefab, shootPoint.position, Quaternion.identity);

                spike.gameObject.SetActive(true);

                // Ajusta la dirección y velocidad del pincho
                Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = directionToHero * spikeSpeed;
                }

                // Opcional: destruye el pincho después de un tiempo para optimizar
                Destroy(spike, 5f); // Elimina el pincho después de 5 segundos
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'Hero' en la escena.");
            }
        }
    }
}
