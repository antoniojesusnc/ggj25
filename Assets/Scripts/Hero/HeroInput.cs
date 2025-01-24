using UnityEngine;
using UnityEngine.InputSystem;

namespace ggj25
{
    public class HeroInput : MonoBehaviour
    {
        private InputData _input;

        public Vector2 Momentum { get; private set; } = new Vector2();
        
        private void Start()
        {
            _input = new InputData();

            Subscribe();
        }

        private void Subscribe()
        {
            _input.Enable();
        }

        void Update()
        {
            if (_input.Hero.MovementX.IsPressed())
            {
                OnMovementX();
            }
            
            if (_input.Hero.MovementY.IsPressed())
            {
                OnMovementY();
            }
        }
        
        private void OnMovementX()
        {
            var newX = Mathf.Clamp(Momentum.x + _input.Hero.MovementX.ReadValue<float>(), -1, 1);
            Momentum = new Vector2(newX, Momentum.y);
        }
        
        private void OnMovementY()
        {
            var newY = Mathf.Clamp(Momentum.y + _input.Hero.MovementY.ReadValue<float>(), -1, 1);
            Momentum = new Vector2(Momentum.x, newY);
        }

        public void DeductMomentum(float configFrictionRate)
        {
            Momentum *= configFrictionRate;
        }
    }
}