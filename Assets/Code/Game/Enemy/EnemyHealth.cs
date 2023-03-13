using Code.Factories.Enemies;
using UnityEngine;
using Zenject;

namespace Code.Game.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public UnitType Unit { get; private set; }
        [field: SerializeField] public Transform Current { get; private set; }
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private float _maxHp;
        [SerializeField] private TakeDamageVisualization _takeDamage;
        [SerializeField] private float _currentHp;

        private IEnemiesPool _pool;

        [Inject]
        public void Constructor(IEnemiesPool pool) =>
            _pool = pool;

        public void Reset() =>
            _currentHp = _maxHp;

        public void ChangeTypeUnit(UnitType unit) =>
            Unit = unit;

        public void TakeDamage(float damage)
        {
            if (_currentHp <= 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;
            _takeDamage.Visualization();

            if (_currentHp == 0)
            {
                _enemyView.gameObject.SetActive(false);
                _pool.UnSpawn(_enemyView);
            }
        }
    }
}