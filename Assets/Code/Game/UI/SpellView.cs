using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _lockImage;
        [SerializeField] private TextMeshProUGUI _text;

        public event Action ClickHandler;

        private float _nextTime;
        private float _cooldown;

        private void Start()
        {
            _button.onClick.AddListener(ClickOnButton);
            UpdateFillAmount();
        }

        private void Update() =>
            UpdateFillAmount();

        private void OnDestroy() =>
            _button.onClick.RemoveListener(ClickOnButton);

        public void SetData(float cooldown, string text)
        {
            _cooldown = cooldown;
            _text.text = text;
        }

        private void UpdateFillAmount()
        {
            float current = _nextTime - Time.time;
            current = current < 0 ? 0 : current;
            SetValue(current, _cooldown);
        }

        private void SetValue(float current, float max) =>
            _lockImage.fillAmount = current / max;

        private void ClickOnButton()
        {
            if (_lockImage.fillAmount > 0)
                return;

            _nextTime = Time.time + _cooldown;
            ClickHandler?.Invoke();
        }
    }
}