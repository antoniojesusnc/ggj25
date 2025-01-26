using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj25
{
    public class BossController : EnemyBase
    {
        [SerializeField] private BossConfig _bossConfig;

        private float _patternShootTimer;

        private void Update()
        {
            if (!_room.IsActive) return;

            // Handle pattern shooting
            _patternShootTimer += Time.deltaTime;
            if (_patternShootTimer >= _bossConfig.PatternShootFrequency)
            {
                ShootPattern();
                _patternShootTimer = 0f;
            }
        }

        private void ShootPattern()
        {
            float angleStep = _bossConfig.PatternSpreadAngle / (_bossConfig.BulletsPerPattern - 1);
            float startAngle = -_bossConfig.PatternSpreadAngle / 2;

            for (int i = 0; i < _bossConfig.BulletsPerPattern; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * Vector2.right;
                
                GameObject spike = Instantiate(spikePrefab, shootPoint.position, Quaternion.identity);
                spike.gameObject.SetActive(true);

                Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * spikeSpeed;
                }

                Destroy(spike, 5f);
            }
        }
    }}