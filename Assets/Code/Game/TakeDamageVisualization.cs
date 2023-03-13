using System;
using Code.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Code.Game
{
    public class TakeDamageVisualization : MonoBehaviour
    {
        [SerializeField] private Coloring[] _colorings;
        [SerializeField] private float _duration = .1f;

        private Sequence _sequence;

        private void OnDestroy() =>
            _sequence.SimpleKill();

        public void Visualization()
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

        [Serializable]
        private class Coloring
        {
            public Color DefaultColor;
            public Color TargetColor;
            public SpriteRenderer Sprite;
        }
    }
}