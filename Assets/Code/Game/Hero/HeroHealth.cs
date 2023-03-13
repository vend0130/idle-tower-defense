using System;
using Code.Data;
using Code.Game.UI;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public Transform Current { get; private set; }

        [SerializeField] private TakeDamageVisualization _takeDamage;

        public event Action DieHandler;

        private float _maxHp;
        private HPBar _hpBar;
        private float _currentHp;

        [Inject]
        public void Constructor(HPBar hpBar, GameData _gameData)
        {
            _hpBar = hpBar;
            _maxHp = _gameData.HeroDefaultHp;

            _currentHp = _maxHp;
            ChangeHPBar();
        }

        public void TakeDamage(float damage)
        {
            if (_currentHp == 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;

            _takeDamage.Visualization();
            ChangeHPBar();

            if (_currentHp <= 0)
            {
                DieHandler?.Invoke();
                Destroy(gameObject);
            }
        }

        private void ChangeHPBar() =>
            _hpBar.SetValue(_currentHp, _maxHp);
    }
}