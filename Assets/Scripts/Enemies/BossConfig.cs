using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj25
{
    [CreateAssetMenu(fileName = "BossConfig", menuName = "GGJ25/BossConfig", order = 1)]
    public class BossConfig : EnemyConfig
    {
        [field: Header("Additional Boss Parameters")]
        [field: SerializeField]
        public float PatternShootFrequency { get; private set; }

        [field: SerializeField] public float TargetedShootFrequency { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public int BulletsPerPattern { get; private set; }
        [field: SerializeField] public float PatternSpreadAngle { get; private set; }
    }
}
