using UnityEngine;
using UnityEngine.InputSystem;

namespace ggj25
{
    public class HeroInput : MonoBehaviour
    {
        private InputData _input;

        public Vector2 Momentum { get; private set; } = new Vector2();
        public bool IsPressed { get; private set; }
        
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
            IsPressed = false;
            if (_input.Hero.MovementX.IsPressed() || _input.Hero.MovementY.IsPressed())
            {
                Momentum = Vector2.zero;
                OnMovementX(_input.Hero.MovementX.ReadValue<float>());
                OnMovementY(_input.Hero.MovementY.ReadValue<float>());
            }
            
            if (_input.Hero.MovementTest.IsPressed())
            {
                Momentum = Vector2.zero;
                var vector2 = _input.Hero.MovementTest.ReadValue<Vector2>();
                OnMovementX(vector2.x);
                OnMovementY(vector2.y);
            }
        }
        
        private void OnMovementX(float value)
        {
            var newX = Mathf.Clamp(Momentum.x + value, -1, 1);
            Momentum = new Vector2(newX, Momentum.y);
            IsPressed = true;
        }
        
        private void OnMovementY(float value)
        {
            var newY = Mathf.Clamp(Momentum.y + value, -1, 1);
            Momentum = new Vector2(Momentum.x, newY);
            IsPressed = true;
        }

        public void DeductMomentum(float configFrictionRate)
        {
            Momentum *= configFrictionRate;
        }
    }
}