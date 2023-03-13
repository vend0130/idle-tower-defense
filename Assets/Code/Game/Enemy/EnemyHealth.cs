using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public Transform Current { get; private set; }
        [SerializeField] private float _maxHp;
        [SerializeField] private TakeDamageVisualization _takeDamage;

        private float _currentHp;

        private void Awake()
        {
            _currentHp = _maxHp;
        }

        public void TakeDamage(float damage)
        {
            if (_currentHp <= 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;
            _takeDamage.Visualization();

            if (_currentHp == 0)
            {
                Debug.Log("Death");
            }
        }
    }
}