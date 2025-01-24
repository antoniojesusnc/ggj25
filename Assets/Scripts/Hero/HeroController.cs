using UnityEngine;

namespace ggj25
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField] private HeroConfig _config;
        private HeroInput _heroInput;

        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            _heroInput = GetComponent<HeroInput>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            var input = _heroInput.Momentum * (_config.Speed * Time.deltaTime);
            transform.Translate(input);

            _heroInput.DeductMomentum(_config.FrictionRate);
        }
    }
}