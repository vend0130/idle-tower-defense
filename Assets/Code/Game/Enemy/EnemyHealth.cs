using Code.Data;
using Code.Factories.Enemies;
using Code.Game.Hero;
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
        private BloodlustData _bloodlustData;
        private IVampire _vampire;

        [Inject]
        public void Constructor(IEnemiesPool pool, IVampire vampire, BloodlustData bloodlustData)
        {
            _pool = pool;
            _vampire = vampire;
            _bloodlustData = bloodlustData;
        }

        public void Reset()
        {
            _currentHp = _maxHp;
            _hpBar.gameObject.SetActive(false);
            _hpBar.SetValue(_currentHp, _maxHp);
        }

        public void ChangeTypeUnit(UnitType unit) =>
            Unit = unit;

        public void TakeDamage(float damage, bool bloodlust = false)
        {
            if (_currentHp <= 0)
                return;

            if (bloodlust)
            {
                _vampire.Vampirism(_maxHp);
                damage += _maxHp * ((float)_bloodlustData.AdditionalDamage / 100);
            }

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