using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ggj25
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private EnemyConfig _enemyConfig;

        private float _timeStamp;

        private bool _jumping;

        private Rigidbody2D _rigidbody2D;
        
        private void Start()
        {
            WaitForJump();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_jumping)
            {
                CheckJumpStop();
                return;
            }
            
            _timeStamp -= Time.deltaTime;
            if (_timeStamp < 0)
            {
                Jump();
            }
            _rigidbody2D.velocity *= _enemyConfig.Friction;
        }

        private void CheckJumpStop()
        {
            if (_rigidbody2D.velocity.magnitude < _enemyConfig.SpeedToConsiderStopped)
            {
                WaitForJump();
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        private void WaitForJump()
        {
            _timeStamp = _enemyConfig.JumpInterval;
            _jumping = false;
        }

        private void Jump()
        {
            _jumping = true;
            var jump = Random.insideUnitCircle * _enemyConfig.JumpDistance;
            _rigidbody2D.AddForce(jump);
        }
    }
}
