using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(ControlOverEnemyData),
        menuName = "Static Data/Spells/" + nameof(ControlOverEnemyData))]
    public class ControlOverEnemyData : SpellData
    {
        [field: SerializeField] public float Cooldown { get; private set; } = 10;
        [SerializeField] private float _timeControlled = 5;

        public int TimeControlled => (int)(_timeControlled * 1000);
    }
}