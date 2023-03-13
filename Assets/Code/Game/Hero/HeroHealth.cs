using System;
using Code.Data;
using Code.Extensions;
using Code.Game.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Game.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private Coloring[] _colorings;
        [SerializeField] private float _duration;

        public event Action DieHandler;

        private float _maxHp;
        private HPBar _hpBar;
        private float _currentHp;
        private Sequence _sequence;

        [Inject]
        public void Constructor(HPBar hpBar, GameData _gameData)
        {
            _hpBar = hpBar;
            _maxHp = _gameData.HeroDefaultHp;

            _currentHp = _maxHp;
            ChangeHPBar();
        }

        private void OnDestroy() =>
            _sequence.SimpleKill();

        public void TakeDamage(float damage)
        {
            if (_currentHp == 0)
                return;

            _currentHp = _currentHp - damage < 0 ? 0 : _currentHp - damage;

            Visualization();
            ChangeHPBar();

            if (_currentHp <= 0)
            {
                DieHandler?.Invoke();
                Destroy(gameObject);
            }
        }

        private void Visualization()
        {
            _sequence.SimpleKill();
            _sequence = DOTween.Sequence();

            ToTarget();
            ToDefault();
        }

        private void ToTarget()
        {
            for (int i = 0; i < _colorings.Length; i++)
            {
                if (i == 0)
                    _sequence.Append(_colorings[0].Sprite.DOColor(_colorings[0].TargetColor, _duration));
                else
                    _sequence.Join(_colorings[i].Sprite.DOColor(_colorings[i].TargetColor, _duration));
            }
        }

        private void ToDefault()
        {
            for (int i = 0; i < _colorings.Length; i++)
            {
                if (i == 0)
                    _sequence.Append(_colorings[0].Sprite.DOColor(_colorings[0].DefaultColor, _duration));
                else
                    _sequence.Join(_colorings[i].Sprite.DOColor(_colorings[i].DefaultColor, _duration));
            }
        }

        private void ChangeHPBar() =>
            _hpBar.SetValue(_currentHp, _maxHp);

        [Serializable]
        private class Coloring
        {
            public Color DefaultColor;
            public Color TargetColor;
            public SpriteRenderer Sprite;
        }
    }
}