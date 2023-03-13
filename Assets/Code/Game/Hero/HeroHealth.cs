using System;
using Code.Data;
using Code.Game.UI;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IVampire
    {
        [field: SerializeField] public UnitType Unit { get; private set; }
        [field: SerializeField] public Transform Current { get; private set; }

        [SerializeField] private TakeDamageVisualization _takeDamage;
        [SerializeField] private float _maxHp;
        [SerializeField] private float _currentHp;

        public event Action DieHandler;

        private HPBar _hpBar;
        private BloodlustData _bloodlustData;

        [Inject]
        public void Constructor(HPBar hpBar, GameData gameData, BloodlustData bloodlustData)
        {
            _hpBar = hpBar;
            _bloodlustData = bloodlustData;
            _maxHp = gameData.HeroDefaultHp;

            _currentHp = _maxHp;
            ChangeHPBar();
        }

        public void TakeDamage(float damage, bool bloodlust = false)
        {
            if (_currentHp == 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;

            ChangeHPBar();
            _takeDamage.Visualization();

            if (_currentHp <= 0)
            {
                DieHandler?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Vampirism(float maxHp)
        {
            _currentHp += maxHp * ((float)_bloodlustData.Vampirism / 100);
            _currentHp = _currentHp > _maxHp ? _maxHp : _currentHp;
            ChangeHPBar();
        }

        private void ChangeHPBar() =>
            _hpBar.SetValue(_currentHp, _maxHp);
    }
}