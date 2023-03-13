using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(MeteoriteData),
        menuName = "Static Data/Spells/" + nameof(MeteoriteData), order = 0)]
    public class MeteoriteData : ScriptableObject
    {
        [field: SerializeField] public float Cooldown { get; private set; } = 10;
        [field: SerializeField] public string Text { get; private set; } = "Метеорит";
        [field: SerializeField] public float Speed { get; private set; } = 20;
    }
}