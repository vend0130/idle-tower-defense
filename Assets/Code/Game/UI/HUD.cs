using UnityEngine;

namespace Code.Game.UI
{
    public class HUD : MonoBehaviour
    {
        public void Hide() =>
            gameObject.SetActive(false);
    }
}