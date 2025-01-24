using UnityEngine;

namespace ggj25
{
    public class HeroController : MonoBehaviour
    {
        [field: SerializeField] 
        public HeroConfig Config { get; private set; }
        
        private HeroInput _heroInput;

        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            _heroInput = GetComponent<HeroInput>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            var input = _heroInput.Momentum * (Config.Speed * Time.deltaTime);
            transform.Translate(input);

            _heroInput.DeductMomentum(Config.FrictionRate);
        }
    }
}