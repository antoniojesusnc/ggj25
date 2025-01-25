using UnityEngine;

namespace ggj25
{
    public class HeroController : MonoBehaviour
    {
        [field: SerializeField] 
        public HeroConfig Config { get; private set; }
        [field: SerializeField] 
        public SpriteRenderer MainArt { get; private set; }
        
        private HeroInput _heroInput;

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