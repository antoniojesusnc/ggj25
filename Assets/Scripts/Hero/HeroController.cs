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
    }
}