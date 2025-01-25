using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace ggj25
{
    public class HeroController : MonoBehaviour
    {
        [field: SerializeField] 
        public HeroConfig Config { get; private set; }
        [field: SerializeField] 
        public SpriteRenderer MainArt { get; private set; }

        [SerializeField] private Sprite upSprite;    // Sprite mirando hacia arriba
        [SerializeField] private Sprite downSprite;  // Sprite mirando hacia abajo
        [SerializeField] private Sprite sideSprite;  // Sprite mirando hacia los lados

        private HeroInput _heroInput;

        [SerializeField] private Transform shield;

        private const int LAYER_SPIKE = 8;

        private void Start()
        {
            _heroInput = GetComponent<HeroInput>();
        }

        private void LateUpdate()
        {
            var input = _heroInput.Momentum * (Config.Speed * Time.deltaTime);
            transform.Translate(input);

            if (!_heroInput.IsPressed)
            {
                _heroInput.DeductMomentum(Config.FrictionRate);
            }
            UpdateSpriteDirection();
        }

        void UpdateSpriteDirection()
        {
            // Calcula la dirección basada en la posición del escudo respecto al héroe
            Vector3 direction = (shield.position - transform.position).normalized;

            // Determina el ángulo en grados
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Selecciona el sprite según el ángulo
            if (angle > 45 && angle <= 135)
            {
                // Mirando hacia arriba
                MainArt.sprite = upSprite;
            }
            else if (angle <= -45 && angle > -135)
            {
                // Mirando hacia abajo
                MainArt.sprite = downSprite;
            }
            else
            {
                // Mirando hacia los lados
                MainArt.sprite = sideSprite;

                if ((angle >= 140 && angle <= 180) || (angle <= -140 && angle >= -180))
                {
                    MainArt.flipX = false; // Mirando a la izquierda
                }
                else if ((angle >= 0 && angle <= 40) || (angle <= 0 && angle >= -40))
                {
                    MainArt.flipX = true; // Mirando a la derecha
                }
            }
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            // Detectar colisión con layers específicas
            int collisionLayer = collision.gameObject.layer;

            if (collisionLayer == LAYER_SPIKE)
            {
                Debug.Log("muelto pol: " + collision.gameObject.name);
                GameManager.Instance.GameOver(false);
            }
        }
    }
}