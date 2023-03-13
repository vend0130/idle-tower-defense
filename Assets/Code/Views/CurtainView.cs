﻿using Code.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Views
{
    public class CurtainView : MonoBehaviour, ICurtain
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _durationOn = 0.15f;
        [SerializeField] private float _durationOff = .2f;

        private const float FadeOnValue = 1f;
        private const float FadeOffValue = 0f;

        private Tween _tween;

        private void OnDestroy()
        {
            _tween.SimpleKill();
        }

        public async UniTask FadeOn()
        {
            gameObject.SetActive(true);
            await Fade(FadeOnValue, _durationOn);
        }

        public async UniTask FadeOff()
        {
            await Fade(FadeOffValue, _durationOff);
            gameObject.SetActive(false);
        }

        private async UniTask Fade(float targetValue, float duration)
        {
            _tween.SimpleKill();
            _tween = _canvasGroup.DOFade(targetValue, duration);
            await _tween.AsyncWaitForCompletion();
        }
    }
}