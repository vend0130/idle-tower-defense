using Code.Factories.Enemies;
using Code.Game.UI;
using UnityEngine;
using Zenject;

namespace Code.Game.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public UnitType Unit { get; private set; }
        [field: SerializeField] public Transform Current { get; private set; }
        [SerializeField] private HPBar _hpBar;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private float _maxHp;
        [SerializeField] private TakeDamageVisualization _takeDamage;
        [SerializeField] private float _currentHp;

        private IEnemiesPool _pool;

        [Inject]
        public void Constructor(IEnemiesPool pool) =>
            _pool = pool;

        public void Reset()
        {
            _currentHp = _maxHp;
            _hpBar.gameObject.SetActive(false);
            _hpBar.SetValue(_currentHp, _maxHp);
        }

        public void ChangeTypeUnit(UnitType unit) =>
            Unit = unit;

        public void TakeDamage(float damage)
        {
            if (_currentHp <= 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;

            _hpBar.gameObject.SetActive(true);
            _hpBar.SetValue(_currentHp, _maxHp);

            _takeDamage.Visualization();

            if (_currentHp == 0)
            {
                _enemyView.gameObject.SetActive(false);
                _pool.UnSpawn(_enemyView);
            }
        }
    }
}