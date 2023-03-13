using UnityEngine;

namespace Code.Game.UI
{
    public class HUD : MonoBehaviour
    {
        [field: SerializeField] public Transform SpellsParent { get; private set; }

        public void Hide() =>
            gameObject.SetActive(false);
    }
}