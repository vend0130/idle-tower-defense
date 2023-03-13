using UnityEngine;

namespace Code.Game.Enemy
{
    public class ControlledView : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private SpriteRenderer _sprite;

        [field: SerializeField] public bool IsControlled { get; private set; }

        public void ChangeControlledState(bool value)
        {
            _sprite.enabled = value;
            IsControlled = value;

            if (value)
            {
                _health.ChangeTypeUnit(UnitType.Hero);
                _attack.ChangeTarget(UnitType.Enemy);
            }
            else
            {
                _health.ChangeTypeUnit(UnitType.Enemy);
                _attack.ChangeTarget(UnitType.Hero);
            }
        }
    }
}