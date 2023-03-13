using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _currentImage;

        public void SetValue(float current, float max) =>
            _currentImage.fillAmount = current / max;
    }
}